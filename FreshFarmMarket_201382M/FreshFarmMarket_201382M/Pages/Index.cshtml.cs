using FreshFarmMarket_201382M.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshFarmMarket_201382M.Pages
{
    public class IndexModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public List<string> ProfileInfo { get; set; } = new();

        public void OnGet()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (userId != null)
            {
                ApplicationUser user = userManager.FindByIdAsync(userId).Result;
                ProfileInfo.Add(user.FullName);

                var dataProtectionProvider = DataProtectionProvider.Create("AY2022/S2");
                var protector = dataProtectionProvider.CreateProtector("FFMSecretKey_AY2022/S2");
                string encCreditCardNo = user.CreditCardNo.ToString();
                string CreditCardNo = protector.Unprotect(encCreditCardNo);
                ProfileInfo.Add(CreditCardNo);

                ProfileInfo.Add(user.Gender);
                ProfileInfo.Add(user.PhoneNumber);
                ProfileInfo.Add(user.DeliveryAddress);
                ProfileInfo.Add(user.NormalizedEmail);
                ProfileInfo.Add(user.PhotoURL);
                ProfileInfo.Add(user.AboutMe);
            }
        }
    }
}
