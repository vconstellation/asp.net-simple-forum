using System.ComponentModel.DataAnnotations;

namespace SimpleForum.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Has to be the same as the password")]
        public string ConfirmPassword { get; set; }
    }
}
