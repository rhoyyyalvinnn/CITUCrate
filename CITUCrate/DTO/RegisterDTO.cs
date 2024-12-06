using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CITUCrate.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [CustomEmailValidation(ErrorMessage = "Email must end with @cit.edu.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class CustomEmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            if (email != null && Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@cit\.edu$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Invalid email domain.");
        }
    }
}
