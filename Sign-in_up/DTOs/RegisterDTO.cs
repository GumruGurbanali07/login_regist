using System.ComponentModel.DataAnnotations;

namespace Sign_in_up.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		public string PasswordConfirmation { get; set; }
    }
}
