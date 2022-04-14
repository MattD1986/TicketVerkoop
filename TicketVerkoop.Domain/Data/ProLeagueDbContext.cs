using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TicketVerkoop.Domain.Entities;

namespace TicketVerkoop.Domain.Data
{
    public partial class ProLeagueDbContext : DbContext
    {
        public ProLeagueDbContext()
        {
        }

        public ProLeagueDbContext(DbContextOptions<ProLeagueDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abonnement> Abonnements { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Club> Clubs { get; set; } = null!;
        public virtual DbSet<Competitie> Competities { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Plaat> Plaats { get; set; } = null!;
        public virtual DbSet<Stadion> Stadions { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<Vak> Vaks { get; set; } = null!;
        public virtual DbSet<VakOmschrijving> VakOmschrijvings { get; set; } = null!;
        public virtual DbSet<Wedstrijd> Wedstrijds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQL19_VIVES; Database=ProLeague; Trusted_Connection=True; MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abonnement>(entity =>
            {
                entity.ToTable("Abonnement");

                entity.Property(e => e.Id)
                    //.ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AankoopDatum)
                    .HasColumnType("datetime")
                    .HasColumnName("aankoopDatum");

                entity.Property(e => e.ClubId).HasColumnName("clubId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.StoelNr).HasColumnName("stoelNr");  

                entity.Property(e => e.VakId).HasColumnName("vakId");

                entity.Property(e => e.Prijs).HasColumnName("prijs");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Abonnements)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonnement_Club");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Abonnements)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonnement_Order");

                entity.HasOne(d => d.Vak)
                    .WithMany(p => p.Abonnements)
                    .HasForeignKey(d => d.VakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonnement_Vak");

            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.ToTable("Club");

                entity.Property(e => e.ClubId).ValueGeneratedNever();

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.Naam).HasMaxLength(50);

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Club_Stadion");
            });

            modelBuilder.Entity<Competitie>(entity =>
            {
                entity.ToTable("Competitie");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EindDatum)
                    .HasColumnType("date")
                    .HasColumnName("eindDatum");

                entity.Property(e => e.Naam)
                    .HasMaxLength(50)
                    .HasColumnName("naam");

                entity.Property(e => e.StartDatum)
                    .HasColumnType("date")
                    .HasColumnName("startDatum");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id)
                    //.ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AankoopDatum)
                    .HasColumnType("datetime")
                    .HasColumnName("aankoopDatum");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(450)
                    .HasColumnName("clientId");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_AspNetUsers");
            });

            modelBuilder.Entity<Plaat>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Stoelnr).HasColumnName("stoelnr");

                entity.Property(e => e.VakId).HasColumnName("vakId");

                entity.Property(e => e.WedstrijdId).HasColumnName("wedstrijdId");

                entity.HasOne(d => d.Vak)
                    .WithMany(p => p.Plaats)
                    .HasForeignKey(d => d.VakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plaats_Vak");
                //entity.Property(e => e.Id)
                //    //.ValueGeneratedNever()
                //    .HasColumnName("id");

                //entity.Property(e => e.VakId).HasColumnName("vakId");

                //entity.Property(e => e.WedstrijdId).HasColumnName("wedstrijdId");

                //entity.HasOne(d => d.Vak)
                //    .WithMany(p => p.Plaats)
                //    .HasForeignKey(d => d.VakId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Plaats_Vak");
            });

            modelBuilder.Entity<Stadion>(entity =>
            {
                entity.ToTable("Stadion");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .HasColumnName("adres");

                entity.Property(e => e.Capaciteit).HasColumnName("capaciteit");

                entity.Property(e => e.Naam)
                    .HasMaxLength(50)
                    .HasColumnName("naam");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(50)
                    .HasColumnName("postcode");

                entity.Property(e => e.Stad)
                    .HasMaxLength(50)
                    .HasColumnName("stad");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.Id)
                    //.ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AankoopDatum)
                    .HasColumnType("datetime")
                    .HasColumnName("aankoopDatum");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.PlaatsId).HasColumnName("plaatsId");

                entity.Property(e => e.WedstrijdId).HasColumnName("wedstrijdId");

                entity.Property(e => e.Prijs).HasColumnName("prijs");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Order");

                entity.HasOne(d => d.Plaats)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.PlaatsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Plaats");

                entity.HasOne(d => d.Wedstrijd)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.WedstrijdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Wedstrijd");
            });

            modelBuilder.Entity<Vak>(entity =>
            {
                entity.ToTable("Vak");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Aantal).HasColumnName("aantal");

                entity.Property(e => e.Prijs).HasColumnName("prijs");

                entity.Property(e => e.StadionId).HasColumnName("stadionId");

                entity.Property(e => e.VakOmschrijvingId).HasColumnName("vakOmschrijvingId");

                entity.HasOne(d => d.Stadion)
                    .WithMany(p => p.Vaks)
                    .HasForeignKey(d => d.StadionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vak_Stadion");

                entity.HasOne(d => d.VakOmschrijving)
                    .WithMany(p => p.Vaks)
                    .HasForeignKey(d => d.VakOmschrijvingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vak_VakOmschrijving");
            });

            modelBuilder.Entity<VakOmschrijving>(entity =>
            {
                entity.ToTable("VakOmschrijving");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Beschrijving)
                    .HasMaxLength(50)
                    .HasColumnName("beschrijving");
            });

            modelBuilder.Entity<Wedstrijd>(entity =>
            {
                entity.ToTable("Wedstrijd");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompetitieId).HasColumnName("competitieID");

                entity.Property(e => e.Datum)
                    .HasColumnType("date")
                    .HasColumnName("datum");

                entity.Property(e => e.Tijd).HasColumnName("tijd");

                entity.HasOne(d => d.Competitie)
                    .WithMany(p => p.Wedstrijds)
                    .HasForeignKey(d => d.CompetitieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wedstrijd_Competitie");

                entity.HasOne(d => d.ThuisPloegNavigation)
                    .WithMany(p => p.WedstrijdThuisPloegNavigations)
                    .HasForeignKey(d => d.ThuisPloeg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wedstrijd_Club1");

                entity.HasOne(d => d.UitPloegNavigation)
                    .WithMany(p => p.WedstrijdUitPloegNavigations)
                    .HasForeignKey(d => d.UitPloeg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wedstrijd_Club");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
