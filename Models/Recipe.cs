using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string ? Title { get; set; }

        [Required]
        [StringLength(400)]
        public string ? Description { get; set; }

        [Required]
        public string? Preparation { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        public TimeSpan PreparationTime { get; set; }
        public int Status { get; set; }

        public int UserId { get; set; }
        public User ?User { get; set; }

        public ICollection<RecipeIngredient> ?RecipeIngredients { get; set; }
        public ICollection<RecipeTag> ?RecipeTags { get; set; }
    }
}
