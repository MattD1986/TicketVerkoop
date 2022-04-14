using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Plaat
    {
        public Plaat()
        {
            //Abonnements = new HashSet<Abonnement>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int VakId { get; set; }
        public int WedstrijdId { get; set; }
        public int Stoelnr { get; set; }

        public virtual Vak Vak { get; set; } = null!;
        //public virtual ICollection<Abonnement> Abonnements { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
