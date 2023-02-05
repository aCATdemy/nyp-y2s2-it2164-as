using FreshFarmMarket_201382M.Model;
using FreshFarmMarket_201382M.Services;
using FreshFarmMarket_201382M.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshFarmMarket_201382M.Pages.Membership
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly CaptchaService _captchaService;

        public LoginModel(SignInManager<ApplicationUser> signInManager, CaptchaService captchaService)
        {
            this.signInManager = signInManager;
            _captchaService = captchaService;
        }

        public void OnGet()
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            // Google reCAPTCHA
            var captchaResult = await _captchaService.VerifyToken(LModel.Token);
            if (!captchaResult)
            {
                TempData["CaptchaMessage.Type"] = "info";
                TempData["CaptchaMessage.Text"] = string.Format("reCAPTCHA validation failed. Please try again.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.EmailAddress, LModel.Password, LModel.RememberMe, lockoutOnFailure: true);
                if (identityResult.IsLockedOut)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("The system detected multiple failed login attempts and locked your account for security reasons. Please try again later.");
                    return Page();
                }
                if (identityResult.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Incorrect username or password.");
            }
            return Page();
        }
    }
}
