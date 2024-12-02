using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class RecipeIngredient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ? Amount { get; set; }

        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public Ingredient ? Ingredient { get; set; }
        public Recipe ? Recipe { get; set; }
    }
}
