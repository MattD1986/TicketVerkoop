using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domain.Entities
{
    public partial class Club
    {
        public Club()
        {
            Abonnements = new HashSet<Abonnement>();
            WedstrijdThuisPloegNavigations = new HashSet<Wedstrijd>();
            WedstrijdUitPloegNavigations = new HashSet<Wedstrijd>();
        }

        public int ClubId { get; set; }
        public string Naam { get; set; } = null!;
        public string? Logo { get; set; }
        public int StadionId { get; set; }

        public virtual Stadion Stadion { get; set; } = null!;
        public virtual ICollection<Abonnement> Abonnements { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdThuisPloegNavigations { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdUitPloegNavigations { get; set; }
    }
}
