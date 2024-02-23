using System.ComponentModel.DataAnnotations;

namespace CreditApplicationMVCProject.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}
