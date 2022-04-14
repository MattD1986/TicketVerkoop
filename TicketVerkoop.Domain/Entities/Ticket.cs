using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int WedstrijdId { get; set; }
        public DateTime AankoopDatum { get; set; }
        public int PlaatsId { get; set; }
        public int OrderId { get; set; }
        public double Prijs { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Plaat Plaats { get; set; } = null!;
        public virtual Wedstrijd Wedstrijd { get; set; } = null!;
    }
}
