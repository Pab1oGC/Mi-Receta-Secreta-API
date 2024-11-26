using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70,MinimumLength = 2)]
        public string ? Name { get; set; }

        [Required]
        [StringLength(300,MinimumLength = 2)]
        public string ? Description { get; set; }

        [Required]
        [StringLength(100,MinimumLength = 2)]
        public string ? Type { get; set; }

        public int Status { get; set; } 

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
