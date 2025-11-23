using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Models; // Proje adına göre güncellendi

namespace ODEVDAGITIM06.Data // Proje adına göre güncellendi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Odev> Odevler { get; set; }
        public DbSet<Teslim> Teslimler { get; set; }
    }
}