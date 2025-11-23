using Microsoft.EntityFrameworkCore; // .Include() METODU İÇİN BU GEREKLİ
using ODEVDAGITIM06.Data;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;

namespace ODEVDAGITIM06.Repositories
{
    // Hem Repository<Odev>'den temel metotları alır
    // hem de IOdevRepository sözleşmesini uygular.
    public class OdevRepository : Repository<Odev>, IOdevRepository
    {
        public OdevRepository(ApplicationDbContext context) : base(context)
        {
            // Base sınıfa (Repository<T>) context'i gönderiyoruz.
        }

        // YENİ EKLENDİ: IOdevRepository'den gelen yeni metodu burada uyguluyoruz.
        public IEnumerable<Odev> GetAllWithDers()
        {
            // Veritabanından Ödevler tablosunu alırken (.Odevler)
            // .Include(o => o.Ders) komutuyla ilgili 'Ders' tablosundaki verileri de
            // onlara bağlayıp (JOIN) öyle getiriyoruz.
            return _context.Odevler.Include(o => o.Ders).ToList();
        }
    }
}