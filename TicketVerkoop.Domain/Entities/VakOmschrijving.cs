using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class VakOmschrijving
    {
        public VakOmschrijving()
        {
            Vaks = new HashSet<Vak>();
        }

        public int Id { get; set; }
        public string Beschrijving { get; set; } = null!;

        public virtual ICollection<Vak> Vaks { get; set; }
    }
}
