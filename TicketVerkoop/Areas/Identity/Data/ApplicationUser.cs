using Microsoft.AspNetCore.Identity;

namespace Project_FullStack.Areas.Identity.Data
{
    public class ApplicationUser: IdentityUser
    {
        [PersonalData]
        public string Naam { get; set; }
        [PersonalData]
        public string Adres { get; set; }
        [PersonalData]
        public string Postcode { get; set; }
        [PersonalData]
        public string Woonplaats { get; set; }
    }
}
