using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ? Name { get; set; }
        public ICollection<RecipeTag> RecipeTags { get; set; }
    }
}
