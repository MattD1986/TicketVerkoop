using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Abonnement
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        //public int PlaatsId { get; set; }
        public DateTime AankoopDatum { get; set; }
        public int OrderId { get; set; }
        public int StoelNr { get; set; }
        public int VakId { get; set; }
        public double Prijs { get; set; }
        
        

        public virtual Club Club { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual Vak Vak { get; set; } = null!;
    }
}
