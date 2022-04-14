using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Vak
    {
        public Vak()
        {
            Abonnements = new HashSet<Abonnement>();
            Plaats = new HashSet<Plaat>();
        }

        public int Id { get; set; }
        public int VakOmschrijvingId { get; set; }
        public int StadionId { get; set; }
        public int Aantal { get; set; }
        public double Prijs { get; set; }

        public virtual Stadion Stadion { get; set; } = null!;
        public virtual VakOmschrijving VakOmschrijving { get; set; } = null!;
        public virtual ICollection<Abonnement> Abonnements { get; set; }
        public virtual ICollection<Plaat> Plaats { get; set; }
    }
}
