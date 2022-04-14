using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketVerkoop.Domain.Entities;

namespace TicketVerkoop.ViewModels
{
    public class WedstrijdDataVM
{

    public IEnumerable<Wedstrijd> wedstrijden { get; set; }
    public IEnumerable<Stadion> stadions { get; set; }
    public IEnumerable<Vak> vakken { get; set; }
    public int WestrijdId { get; set; }
    public int clubId { get; set; }
    public int stadionId { get; set; }
    public int aantal { get; set; }
    public string stadion { get; set; }
    public string thuisploeg { get; set; }
    public string uitploeg { get; set; }
    public DateTime datum { get; set; }
    public TimeSpan uur { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> clubList { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> vakkenList { get; set; }
    public int vakId { get; set; }

}
}
