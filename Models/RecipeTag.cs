using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class RecipeTag
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int TagId { get; set; }
        public Recipe ? Recipe { get; set; }
        public Tag ? Tag { get; set; }
    }
}
