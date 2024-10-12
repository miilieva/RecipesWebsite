using System;
using System.Collections.Generic;

namespace Recipes.Models;

public partial class RecipeStep
{
    public int Id { get; set; }

    public int SortOrder { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
