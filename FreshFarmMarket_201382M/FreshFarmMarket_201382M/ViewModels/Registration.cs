using System.ComponentModel.DataAnnotations;

namespace FreshFarmMarket_201382M.ViewModels
{
    public class Registration
    {
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your full name.")]
        [Display(Name = "Full Name")]
        [DataType(DataType.Text)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your credit card number.")]
        [Display(Name = "Credit Card Number")]
        [CreditCard(ErrorMessage = "Invalid credit card number.")]
        [DataType(DataType.Text)]
        public string CreditCardNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select your gender.")]
        [Display(Name = "Gender")]
        [DataType(DataType.Text)]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your mobile number.")]
        [MinLength(8, ErrorMessage = "Invalid mobile number."), MaxLength(8, ErrorMessage = "Invalid mobile number.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid mobile number.")]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.Text)]
        public string MobileNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your delivery address.")]
        [Display(Name = "Delivery Address")]
        [DataType(DataType.Text)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email address.")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}$", ErrorMessage = "Password must be at least 12 characters long, containing one upper-case letter, one lower-case character, and one special character.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please re-enter your password.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Your password and confirm password does not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Photo")]
        public string PhotoURL { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a short description of yourself.")]
        [Display(Name = "About Me")]
        public string AboutMe { get; set; } = string.Empty;
    }
}
