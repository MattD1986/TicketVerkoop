using Microsoft.AspNetCore.Mvc.Rendering;
using TicketVerkoop.Domain.Entities;

namespace TicketVerkoop.ViewModels
{
    public class PloegVM
    {
        public int clubId { get; set; }
        public string clubNaam { get; set; }
        public Competitie competitie { get; set; }
        public IEnumerable<SelectListItem> vakkenList { get; set; }
        public int vakId { get; set; }
    }
}
