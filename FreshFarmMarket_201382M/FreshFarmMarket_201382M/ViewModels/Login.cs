using System.ComponentModel.DataAnnotations;

namespace FreshFarmMarket_201382M.ViewModels
{
    public class Login
    {
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter an email address.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
