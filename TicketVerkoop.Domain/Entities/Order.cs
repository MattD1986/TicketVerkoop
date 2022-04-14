using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            Abonnements = new HashSet<Abonnement>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string ClientId { get; set; } = null!;
        public DateTime AankoopDatum { get; set; }

        public virtual AspNetUser Client { get; set; } = null!;
        public virtual ICollection<Abonnement> Abonnements { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
