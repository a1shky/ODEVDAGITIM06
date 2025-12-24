using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Identity için gerekli
using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Models;

namespace ODEVDAGITIM06.Data
{
    // DİKKAT: Artık 'DbContext' değil, 'IdentityDbContext<ApplicationUser>'den miras alıyoruz.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Repositorylerde genelde tekil isim (_context.Teslim gibi) kullandığımız için 
        // buradaki isimleri de TEKİL (Singular) yaptık.

        public DbSet<Ders> Ders { get; set; }    // Dersler -> Ders oldu
        public DbSet<Odev> Odev { get; set; }    // Odevler -> Odev oldu
        public DbSet<Teslim> Teslim { get; set; } // Teslimler -> Teslim oldu (HATAYI ÇÖZEN KISIM)
    }
}