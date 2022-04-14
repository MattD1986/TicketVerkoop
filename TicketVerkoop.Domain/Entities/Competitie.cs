using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Competitie
    {
        public Competitie()
        {
            Wedstrijds = new HashSet<Wedstrijd>();
        }

        public int Id { get; set; }
        public string Naam { get; set; } = null!;
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }

        public virtual ICollection<Wedstrijd> Wedstrijds { get; set; }
    }
}
