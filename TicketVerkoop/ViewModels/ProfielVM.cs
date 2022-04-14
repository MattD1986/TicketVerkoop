using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TicketVerkoop.ViewModels
{
    public class ProfielVM
    {
        [Required(ErrorMessage = "Gelieve een emailadres in te voeren")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "U dient een paswoord in te geven")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gelieve een adres in te voeren")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Gelieve een geldige postcode in te voeren")]
        [Range(1000, 9999, ErrorMessage = "valid zip codes range from 1000 to 9999")]
        public string Postcode { get; set; }
        [Required(ErrorMessage = "Gelieve uw woonplaats in te voeren")]
        public string Woonplaats { get; set; }


    }
}
