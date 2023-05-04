using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ServiceApp.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



        public DbSet<Kurir> Kuriri { get; set; }

        public DbSet<Paket> Paketi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kurir>().HasData(
                new Kurir { Id = 1, Ime = "Marko Petrov", GodinaRodjenja = 1987 },
                new Kurir { Id = 2, Ime = "Andrea Marin", GodinaRodjenja = 1990 },
                new Kurir { Id = 3, Ime = "Viktor Pavlov", GodinaRodjenja = 1987 }
                );

            modelBuilder.Entity<Paket>().HasData(
                new Paket { Id = 1, Posiljalac = "Slike doo", Primalac = "Galerija doo", Tezina = 1.1, CenaPostarine = 520, KurirId = 3 },
                new Paket { Id = 2, Posiljalac = "Galerija doo", Primalac = "Salon Centar", Tezina = 0.9, CenaPostarine = 340, KurirId = 1 },
                new Paket { Id = 3, Posiljalac = "Best terarijumi", Primalac = "Ljubimci sur", Tezina = 5.4, CenaPostarine = 2200, KurirId = 2 },
                new Paket { Id = 4, Posiljalac = "Kul klime", Primalac = "Izgradnja doo", Tezina = 7.8, CenaPostarine = 4500, KurirId = 3 },
                new Paket { Id = 5, Posiljalac = "Galanterija szr", Primalac = "Prav kroj szr", Tezina = 2.2, CenaPostarine = 800, KurirId = 1 }
                );

            base.OnModelCreating(modelBuilder);
        }
            

    }
}
