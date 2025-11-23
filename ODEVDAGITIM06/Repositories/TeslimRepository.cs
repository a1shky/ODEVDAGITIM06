using Microsoft.EntityFrameworkCore; // .Include() METODU İÇİN BU GEREKLİ
using ODEVDAGITIM06.Data;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;

namespace ODEVDAGITIM06.Repositories
{
    // Hem Repository<Teslim>'den temel metotları alır
    // hem de ITeslimRepository sözleşmesini uygular.
    public class TeslimRepository : Repository<Teslim>, ITeslimRepository
    {
        public TeslimRepository(ApplicationDbContext context) : base(context)
        {
            // Base sınıfa (Repository<T>) context'i gönderiyoruz.
        }

        // YENİ EKLENDİ: ITeslimRepository'den gelen yeni metodu burada uyguluyoruz.
        public IEnumerable<Teslim> GetAllWithOdevDers()
        {
            // Veritabanından Teslimler tablosunu alırken
            return _context.Teslimler
                .Include(t => t.Odev)         // Teslimin ait olduğu Ödevi de getir
                .Include(t => t.Odev.Ders)    // O Ödevin ait olduğu Dersi de getir
                .ToList();
        }
    }
}