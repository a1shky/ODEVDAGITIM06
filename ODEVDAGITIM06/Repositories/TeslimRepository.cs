using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Data; // Context'in olduğu yer
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ODEVDAGITIM06.Repositories
{
    public class TeslimRepository : Repository<Teslim>, ITeslimRepository
    {
        // Context'e erişmek için
        private readonly ApplicationDbContext _context;

        public TeslimRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Teslim> GetAllWithOdevDers()
        {
            return _context.Teslim
                .Include(t => t.Ogrenci)
                .Include(t => t.Odev).ThenInclude(o => o.Ders)
                .ToList();
        }

        // YENİ EKLEDİĞİMİZ METOT (BURASI KRİTİK)
        public Teslim GetByIdWithDetails(int id)
        {
            return _context.Teslim
                .Include(t => t.Ogrenci)              // Öğrenciyi getir
                .Include(t => t.Odev).ThenInclude(o => o.Ders) // Ödevi ve Dersi getir
                .FirstOrDefault(t => t.TeslimId == id);
        }
    }
}