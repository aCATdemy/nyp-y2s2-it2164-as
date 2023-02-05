using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshFarmMarket_201382M.Pages
{
    [Authorize(Roles = "Member")]
    public class LoungeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
