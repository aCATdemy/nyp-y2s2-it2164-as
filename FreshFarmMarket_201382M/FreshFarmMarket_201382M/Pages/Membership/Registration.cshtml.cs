using FreshFarmMarket_201382M.Model;
using FreshFarmMarket_201382M.Services;
using FreshFarmMarket_201382M.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshFarmMarket_201382M.Pages.Membership
{
    public class RegistrationModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private readonly RoleManager<IdentityRole> roleManager;

        private IWebHostEnvironment _environment;
        private readonly CaptchaService _captchaService;

        [BindProperty]
        public Registration RModel { get; set; }

        [BindProperty]
        public IFormFile? PhotoUpload { get; set; }

        public RegistrationModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment, CaptchaService captchaService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _environment = environment;
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
            var captchaResult = await _captchaService.VerifyToken(RModel.Token);
            if (!captchaResult)
            {
                TempData["CaptchaMessage.Type"] = "info";
                TempData["CaptchaMessage.Text"] = string.Format("reCAPTCHA validation failed. Please try again.");
                return Page();
            }

            if (PhotoUpload == null)
            {
                TempData["UploadMessage.Text"] = string.Format("Please upload a photo.");
            }

            if (PhotoUpload != null)
            {
                var photoFile = Guid.NewGuid() + Path.GetExtension(PhotoUpload.FileName);
                if (Path.GetExtension(photoFile) != ".jpg")
                {
                    TempData["UploadMessage.Text"] = string.Format("The uploaded photo must be in .jpg format.");
                }
            }

            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("AY2022/S2");
                var protector = dataProtectionProvider.CreateProtector("FFMSecretKey_AY2022/S2");

                if (PhotoUpload != null)
                {
                    var uploadsFolder = "uploads";
                    var photoFile = Guid.NewGuid() + Path.GetExtension(PhotoUpload.FileName);
                    if (Path.GetExtension(photoFile) != ".jpg")
                    {
                        TempData["UploadMessage.Text"] = string.Format("The uploaded photo must be in .jpg format.");
                    }
                    else
                    {
                        var photoPath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, photoFile);
                        RModel.PhotoURL = string.Format("/{0}/{1}", uploadsFolder, photoFile);

                        var user = new ApplicationUser()
                        {
                            UserName = RModel.EmailAddress,
                            FullName = RModel.FullName,
                            CreditCardNo = protector.Protect(RModel.CreditCardNo),
                            Gender = RModel.Gender,
                            PhoneNumber = RModel.MobileNo,
                            DeliveryAddress = RModel.DeliveryAddress,
                            Email = RModel.EmailAddress,
                            PhotoURL = RModel.PhotoURL,
                            AboutMe = RModel.AboutMe
                        };

                        IdentityRole role = await roleManager.FindByIdAsync("Member");
                        if (role == null)
                        {
                            IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Member"));
                            if (!result2.Succeeded)
                            {
                                ModelState.AddModelError(string.Empty, "Create role 'Member' failed.");
                            }
                        }

                        var result = await userManager.CreateAsync(user, RModel.Password);
                        if (result.Succeeded)
                        {
                            using var fileStream = new FileStream(photoPath, FileMode.Create);
                            await PhotoUpload.CopyToAsync(fileStream);

                            result = await userManager.AddToRoleAsync(user, "Member");

                            await signInManager.SignInAsync(user, false);
                            return RedirectToPage("/Index");
                        }

                        foreach (var error in result.Errors)
                        {
                            TempData["CaptchaMessage.Type"] = "danger";
                            TempData["CaptchaMessage.Text"] = string.Format(error.Description);
                        }
                    }
                }
            }
            return Page();
        }
    }
}
