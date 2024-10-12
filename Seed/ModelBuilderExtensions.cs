using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Initial recipes
            List<Recipe> recipes = new List<Recipe>();

            var omelettee = new Recipe
            {
                Id = 1,
                Title = "Omelette",
                Category = "Appetizer",
                CreatedDate = DateTime.Now,
                Complexity = "Easy",
                PreparationTime = "5 minutes",
                ImageUrl = "https://i0.wp.com/www.onceuponachef.com/images/2021/12/Omelette.jpg"
            };
            recipes.Add(omelettee);

            var pasta = new Recipe
            {
                Id = 2,
                Title = "Pasta Carbonara",
                Category = "Main Course",
                CreatedDate = DateTime.Now,
                Complexity = "Medium",
                PreparationTime = "30 minutes",
                ImageUrl = "https://www.cookingclassy.com/wp-content/uploads/2020/10/spaghetti-carbonara-01-600x900.jpg"
            };

            recipes.Add(pasta);

            var pancakes = new Recipe
            {
                Id = 3,
                Title = "Pancakes",
                Category = "Desert",
                CreatedDate = DateTime.Now,
                Complexity = "Easy",
                PreparationTime = "10 minutes",
                ImageUrl = "https://www.wholesomeyum.com/wp-content/uploads/2017/03/wholesomeyum-Low-Carb-Keto-Pancakes-Recipe-21.jpg"

            };

            recipes.Add(pancakes);

            modelBuilder.Entity<Recipe>().HasData(recipes);

            // Initials steps and ingridients
            List<RecipeStep> steps = new List<RecipeStep>();
            List<Ingredient> ingredients = new List<Ingredient>();
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

            var eggs = new Ingredient { Id = 1, Title = "Eggs", IsAlergen = true };
            var milk = new Ingredient { Id = 2, Title = "Milk", IsAlergen = true };
            var butter = new Ingredient { Id = 3, Title = "Butter", IsAlergen = true };
            var pastaa = new Ingredient { Id = 4, Title = "Pasta", IsAlergen = true };
            var bacon = new Ingredient { Id = 5, Title = "Bacon", IsAlergen = true };
            var parmesanCheese = new Ingredient { Id = 6, Title = "Parmesan Cheese", IsAlergen = true };
            var heavyCream = new Ingredient { Id = 7, Title = "Heavy Cream", IsAlergen = true };
            var sugar = new Ingredient { Id = 8, Title = "Sugar", IsAlergen = true };
            var flour = new Ingredient { Id = 9, Title = "Flour", IsAlergen = true };
            var bakingPower = new Ingredient { Id = 10, Title = "Baking powder", IsAlergen = true };

            ingredients.AddRange(new List<Ingredient>() { eggs, milk, butter, pastaa, bacon, parmesanCheese, heavyCream, sugar, flour, bakingPower });
            modelBuilder.Entity<Ingredient>().HasData(ingredients);

            var omeletteeStep1 = new RecipeStep { Id = 1, RecipeId = omelettee.Id, Description = "Crack the eggs into a bowl" };
            var omeletteeStep2 = new RecipeStep { Id = 2, RecipeId = omelettee.Id, Description = "Whisk the eggs until they are well beaten" };
            var omeletteeStep3 = new RecipeStep { Id = 3, RecipeId = omelettee.Id, Description = "Add the milk to the eggs and whisk again" };
            var omeletteeStep4 = new RecipeStep { Id = 4, RecipeId = omelettee.Id, Description = "Melt the butter in a non-stick pan" };
            var omeletteeStep5 = new RecipeStep { Id = 5, RecipeId = omelettee.Id, Description = "Pour the egg mixture into the pan and cook until set" };

            

            var omletteIngredient1 = new RecipeIngredient { Id = 1, IngredientId = eggs.Id, RecipeId = omelettee.Id };
            var omletteIngredient2 = new RecipeIngredient { Id = 2, IngredientId = milk.Id, RecipeId = omelettee.Id };
            var omletteIngredient3 = new RecipeIngredient { Id = 3, IngredientId = butter.Id, RecipeId = omelettee.Id };
            //var step = new List<RecipeStep> { omeletteeStep1, omeletteeStep2, omeletteeStep3, omeletteeStep4, omeletteeStep5 };
            steps.AddRange(new List<RecipeStep>() { omeletteeStep1, omeletteeStep2, omeletteeStep3, omeletteeStep4, omeletteeStep5 });
            recipeIngredients.AddRange(new List<RecipeIngredient>() { omletteIngredient1, omletteIngredient2, omletteIngredient3 });


            var pastaStep1 = new RecipeStep { Id = 6, RecipeId = pasta.Id, Description = "Cook the pasta according to the package instructions" };
            var pastaStep2 = new RecipeStep { Id = 7, RecipeId = pasta.Id, Description = "Fry the bacon in a pan until crispy, then chop it into small pieces" };
            var pastaStep3 = new RecipeStep { Id = 8, RecipeId = pasta.Id, Description = "Beat the eggs and Parmesan cheese in a bowl" };
            var pastaStep4 = new RecipeStep { Id = 9, RecipeId = pasta.Id, Description = "Drain the pasta and add it to the pan with the bacon" };
            var pastaStep5 = new RecipeStep { Id = 10, RecipeId = pasta.Id, Description = "Pour the egg mixture over the pasta and stir well" };
            var pastaStep6 = new RecipeStep { Id = 11, RecipeId = pasta.Id, Description = "Add the heavy cream and stir again" };
            var pastaIngredient1 = new RecipeIngredient { Id = 4, IngredientId = pastaa.Id, RecipeId = pasta.Id };
            var pastaIngredient2 = new RecipeIngredient { Id = 5, IngredientId = bacon.Id, RecipeId = pasta.Id };
            var pastaIngredient3 = new RecipeIngredient { Id = 6, IngredientId = eggs.Id, RecipeId = pasta.Id };
            var pastaIngredient4 = new RecipeIngredient { Id = 7, IngredientId = parmesanCheese.Id, RecipeId = pasta.Id };
            var pastaIngredient5 = new RecipeIngredient { Id = 8, IngredientId = heavyCream.Id, RecipeId = pasta.Id };

            steps.AddRange(new List<RecipeStep>() { pastaStep1, pastaStep2, pastaStep3, pastaStep4, pastaStep5, pastaStep6 });
            recipeIngredients.AddRange(new List<RecipeIngredient>() { pastaIngredient1, pastaIngredient2, pastaIngredient3,pastaIngredient4,pastaIngredient5 });

            var pancakesStep1 = new RecipeStep { Id = 12, RecipeId = pancakes.Id, Description = "Combine the dry ingredients" };
            var pancakesStep2 = new RecipeStep { Id = 13, RecipeId = pancakes.Id, Description = "Add the wet ingredients and mix" };
            var pancakesStep3 = new RecipeStep { Id = 14, RecipeId = pancakes.Id, Description = "Pour or ladle the batter onto the oiled griddle or pan" };
            var pancakesStep4 = new RecipeStep { Id = 15, RecipeId = pancakes.Id, Description = "Cook until bubbles form, flip, and cook on the other side" };
            var pancakesIngredient1 = new RecipeIngredient { Id = 9, IngredientId = flour.Id, RecipeId = pancakes.Id };
            var pancakesIngredient2 = new RecipeIngredient { Id = 10, IngredientId = sugar.Id, RecipeId = pancakes.Id };
            var pancakesIngredient3 = new RecipeIngredient { Id = 11, IngredientId = bakingPower.Id, RecipeId = pancakes.Id };
            var pancakesIngredient4 = new RecipeIngredient { Id = 12, IngredientId = milk.Id, RecipeId = pancakes.Id };
            var pancakesIngredient5 = new RecipeIngredient { Id = 13, IngredientId = eggs.Id, RecipeId = pancakes.Id };

            steps.AddRange(new List<RecipeStep>() { pancakesStep1, pancakesStep2, pancakesStep3, pancakesStep4 });
            recipeIngredients.AddRange(new List<RecipeIngredient>() { pancakesIngredient1, pancakesIngredient2, pancakesIngredient3, pancakesIngredient4, pancakesIngredient5 });


            for (var i = 0; i < steps.Count; i++)
            {
                steps.ElementAt(i).SortOrder = i;
            }

            modelBuilder.Entity<RecipeStep>().HasData(steps);
            //modelBuilder.Entity<Ingredient>().HasData(ingredients);
            modelBuilder.Entity<RecipeIngredient>().HasData(recipeIngredients);
        }
    }
}

        /*public static void SeedExample(this ModelBuilder modelBuilder)
        {
            var omelettee = new Recipe
            {
                Id = 1,
                Title = "Omelette",
                Category = "Appetizer",
                Complexity = "Easy",
                PreparationTime = "5 minutes",
                ImageUrl = "https://i0.wp.com/www.onceuponachef.com/images/2021/12/Omelette.jpg"
            };
            modelBuilder.Entity<Recipe>().HasData(omelettee);

            var omeletteeStep1 = new RecipeStep { Id = 1, RecipeId = omelettee.Id, Description = "Crack the eggs into a bowl" };
            var omeletteeStep2 = new RecipeStep { Id = 2, RecipeId = omelettee.Id, Description = "Whisk the eggs until they are well beaten" };
            var omeletteeStep3 = new RecipeStep { Id = 3, RecipeId = omelettee.Id, Description = "Add the milk to the eggs and whisk again" };
            var omeletteeStep4 = new RecipeStep { Id = 4, RecipeId = omelettee.Id, Description = "Melt the butter in a non-stick pan" };
            var omeletteeStep5 = new RecipeStep { Id = 5, RecipeId = omelettee.Id, Description = "Pour the egg mixture into the pan and cook until set" };

            modelBuilder.Entity<RecipeStep>().HasData(omeletteeStep1, omeletteeStep2, omeletteeStep3, omeletteeStep4, omeletteeStep5);

        }
    }
}*/

