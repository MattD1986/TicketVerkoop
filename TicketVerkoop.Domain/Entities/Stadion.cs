using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Stadion
    {
        public Stadion()
        {
            Clubs = new HashSet<Club>();
            Vaks = new HashSet<Vak>();
        }

        public int Id { get; set; }
        public string Naam { get; set; } = null!;
        public int Capaciteit { get; set; }
        public string? Adres { get; set; }
        public string? Postcode { get; set; }
        public string? Stad { get; set; }

        public virtual ICollection<Club> Clubs { get; set; }
        public virtual ICollection<Vak> Vaks { get; set; }
    }
}
