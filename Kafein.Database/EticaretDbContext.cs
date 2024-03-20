using Microsoft.EntityFrameworkCore;
using Kafein.Model;
using Microsoft.Extensions.Configuration;

namespace Kafein.Database
{
    public class EticaretDbContext : DbContext
    {
        private static string _connectionString { get; set; }

        public EticaretDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Eticaret");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Siparis> Siparisler  { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylar  { get; set; }
    }
}