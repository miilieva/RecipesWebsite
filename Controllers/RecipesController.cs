using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Recipes.Models;

namespace Recipes.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RecipesContext _context;

        public RecipesController(RecipesContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'Recipes'  is null.");
            }
            var recipes = from r in _context.Recipes
                          select r;

            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["SearchString"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    recipes = recipes.OrderByDescending(s => s.Title);
                    break;
                default:
                    recipes = recipes.OrderBy(s => s.Title);
                    break;
            }

            return View(await recipes.ToListAsync());
        }


        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.Include(m => m.RecipeSteps)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            var recipeVM = new RecipeViewModel()
            {
                Category = recipe.Category,
                Id = recipe.Id,
                Complexity = recipe.Complexity,
                CreatedDate = recipe.CreatedDate,
                ImageUrl = recipe.ImageUrl,
                PreparationTime = recipe.PreparationTime,
                Title = recipe.Title,
                RecipeSteps = recipe.RecipeSteps.ToList()
            };

            return View(recipeVM);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Category,CreatedDate,Complexity,PreparationTime,ImageUrl")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context
                .Recipes
                .Include(r => r.RecipeSteps)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var ingredientViewModelsList = new List<IngredientViewModel>();

            var ingridients = _context.Ingredients.ToList();
            var rIngridiant = await _context.RecipeIngredients.Include(i => i.Ingredient).Where(ri => ri.RecipeId == id).ToListAsync();
           
            if (ingridients.Any())
            {
                foreach (var ingridient in ingridients)
                {
                    bool isActive = rIngridiant.Any(ri => ri.IngredientId == ingridient.Id);
                    ingredientViewModelsList.Add( new IngredientViewModel { Title = ingridient.Title, Id= ingridient.Id, IsActive = isActive });
                }
            }
       
            var recipeVM = new RecipeViewModel()
            {
                Category = recipe.Category,
                Id = recipe.Id,
                Complexity = recipe.Complexity,
                CreatedDate = recipe.CreatedDate,
                ImageUrl = recipe.ImageUrl,
                PreparationTime = recipe.PreparationTime,
                Title = recipe.Title,
                RecipeSteps = recipe.RecipeSteps.ToList(),
                RecipeIngrediants = ingredientViewModelsList
            };

            return View(recipeVM);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,CreatedDate,Complexity,PreparationTime,ImageUrl,RecipeSteps,RecipeIngrediants")] RecipeViewModel recipe)
        {
            //RecipeIngrediants are now bound => add logic for saving them
            var recipeEntity = await _context
                .Recipes
                .Include(r => r.RecipeSteps)
                .Include(r => r.RecipeIngredients)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (id != recipeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    recipeEntity.Title = recipe.Title;
                    recipeEntity.Complexity = recipe.Complexity;
                    recipeEntity.Category = recipe.Category;
                    recipeEntity.CreatedDate = recipe.CreatedDate;
                    recipeEntity.ImageUrl = recipe.ImageUrl;
                    recipeEntity.PreparationTime = recipe.PreparationTime;

                    foreach (var step in recipe.RecipeSteps)
                    {
                        if (step.Id == 0)
                        {
                            // Add new step
                            recipeEntity.RecipeSteps.Add(new RecipeStep
                            {
                                Description = step.Description,
                                SortOrder = step.SortOrder
                            });
                        }
                        else
                        {
                            // Update existing step
                            var existingStep = recipeEntity.RecipeSteps.FirstOrDefault(s => s.Id == step.Id);
                            if (existingStep != null)
                            {
                                existingStep.Description = step.Description;
                                existingStep.SortOrder = step.SortOrder;
                            }
                        }

                        //add all the rest and implement the logic 

                        _context.Update(recipeEntity);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipeEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }



        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'Recipe'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ClearRecipes()
        {
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { searchString = "" });

        }

        [HttpPost("Recipes/AddRemoveIngredient/{id:int}")]
        public IActionResult AddRemoveIngredient([FromRoute] int id, RecipeViewModel model)
        {
            var ingredient = model.RecipeIngrediants.Single(i => i.Id == id);
            ingredient.IsActive = !ingredient.IsActive;

            return PartialView("Ingredients", model);
        }
    }
}
