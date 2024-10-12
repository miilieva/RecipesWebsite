namespace Recipes.Models;

public partial class RecipeViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? Complexity { get; set; }

    public string? PreparationTime { get; set; }

    public string? ImageUrl { get; set; }

    public List<IngredientViewModel> RecipeIngrediants { get; set; } = new List<IngredientViewModel>();

    public List<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
}
