using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TicketVerkoop.ViewModels
{
    public class ShoppingCartVM
    {
        public List<CartVM>? Cart { get; set; }
        public float totaalPrijs { get; set; } = 0;
    }

    public class CartVM
    {
        public int Id { get; set; }
        public int WedstrijdNr { get; set; }
        public int ClubNr { get; set; }
        public int vakNr { get; set; }
        public DateTime wedstrijdDatum { get; set; }
        [Required]
        public int vakId { get; set; }
        public string NaamThuisPloeg { get; set; }
        public string NaamUitPloeg { get; set; }
        public string Stadion { get; set; }
        public int Aantal { get; set; }
        public float Prijs { get; set; }
        public System.DateTime AanmaakDatum { get; set; }
        public int CompetitieId { get; set; }
       

    }
}
