using System.ComponentModel.DataAnnotations;

namespace MiRecetaSecretaAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Names are required.")]
        [StringLength(70, ErrorMessage = "Names cannot exceed 70 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Names cannot contain numbers or special characters.")]
        public string? Names { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [StringLength(70, ErrorMessage = "Lastname cannot exceed 70 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Lastname cannot contain numbers or special characters.")]
        public string? Lastname { get; set; }

        [StringLength(70, ErrorMessage = "SecondLastname cannot exceed 70 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "SecondLastname cannot contain numbers or special characters.")]
        public string? SecondLastname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one number.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Role cannot contain numbers or special characters.")]
        public string? Rol { get; set; }

        [Range(0, 1, ErrorMessage = "Status must be 0 (inactive) or 1 (active).")]
        public int? Status { get; set; } = 1; 

       
        public int CreatedBy { get; set; }

        public ICollection<Ingredient>? Ingredients { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
