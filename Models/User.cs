using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string ? Names { get; set; }

        [Required]
        [StringLength(70)]
        public string ? Lastname { get; set; }

        [StringLength(70)]
        public string ? SecondLastname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string ? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ? Password { get; set; }

        [Required]
        public string ? Rol { get; set; }
        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
