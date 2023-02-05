using FreshFarmMarket_201382M.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshFarmMarket_201382M.Pages.Shared
{
    public class _LayoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public _LayoutModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }
    }
}
