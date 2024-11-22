using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        [Required]
        public int Amount { get; set; }
        public Ingredient ? Ingredient { get; set; }
        public Recipe ? Recipe { get; set; }
    }
}
