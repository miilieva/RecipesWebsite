using System;
using System.Collections.Generic;

namespace Recipes.Models;

public partial class Ingredient
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool IsAlergen { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
