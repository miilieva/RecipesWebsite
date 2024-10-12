using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? Complexity { get; set; }

    public string? PreparationTime { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
}
