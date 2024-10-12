using System;
using System.Collections.Generic;

namespace Recipes.Models;

public partial class RecipeIngredient
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;

    //public string Title { get; set; }
}
