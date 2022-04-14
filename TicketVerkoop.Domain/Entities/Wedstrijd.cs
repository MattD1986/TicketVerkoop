using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Wedstrijd
    {
        public Wedstrijd()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int ThuisPloeg { get; set; }
        public int UitPloeg { get; set; }
        public int CompetitieId { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Tijd { get; set; }

        public virtual Competitie Competitie { get; set; } = null!;
        public virtual Club ThuisPloegNavigation { get; set; } = null!;
        public virtual Club UitPloegNavigation { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
