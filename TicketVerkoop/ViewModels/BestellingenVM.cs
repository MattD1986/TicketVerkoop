using System.ComponentModel;
using TicketVerkoop.Domain.Entities;

namespace TicketVerkoop.ViewModels
{
    public class BestellingenVM
    {
        public IEnumerable<Abonnement> abonnementen { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        [DisplayName("Club")]
        public string clubNaam { get; set; }
        
        [DisplayName("Vak")]
        public string vakNaam { get; set; }
        //public string vakNummer { get; set; }
        [DisplayName("Stoelnummer")]
        public int plaats { get; set; }
        [DisplayName("Wedstrijd")]   // moet voor de view, omdat thuisploeg & uitploeg in zelfde kolom staan
        public string thuisPloeg { get; set; }
        public string uitPloeg { get; set; }
        public DateTime datum { get; set; }
        public TimeSpan uur { get; set; }
        //public int Stoelnr { get; set; }

        public float prijs { get; set; }
    }
}
