using Microsoft.AspNetCore.Identity;

namespace FreshFarmMarket_201382M.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string CreditCardNo { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string? PhotoURL { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
    }
}
