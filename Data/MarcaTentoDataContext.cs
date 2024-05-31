using MarcaTento.Data.Mappings;
using MarcaTento.Models;
using Microsoft.EntityFrameworkCore;

namespace MarcaTento.Data
{
    public class MarcaTentoDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MarcaTento;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new MatchMap());
        }

    }
}
