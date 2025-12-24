using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Data;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ODEVDAGITIM06.Repositories
{
    public class OdevRepository : Repository<Odev>, IOdevRepository
    {
        private readonly ApplicationDbContext _appContext;

        public OdevRepository(ApplicationDbContext context) : base(context)
        {
            _appContext = context;
        }

        public IEnumerable<Odev> GetAllWithDers()
        {
            return _appContext.Odev
                .Include(o => o.Ders)
                .ToList();
        }

        public IEnumerable<Odev> GetAllWithDersAndOgrenci()
        {
            return _appContext.Odev
                .Include(o => o.Ders)
                .Include(o => o.Ogrenci) // Kişiye özel atamaları görebilmek için
                .ToList();
        }

        // --- İŞTE DÜZELTİLEN METOT ---
        public IEnumerable<Odev> GetOdevlerByOgrenciId(string ogrenciId)
        {
            // ESKİ HATALI KOD: _appContext.Teslim... (Sadece teslim edilenleri getiriyordu)
            // YENİ DOĞRU KOD: _appContext.Odev... (Atanan ödevleri getirir)

            return _appContext.Odev
                .Include(o => o.Ders) // Ders adını görmek için Include
                .Where(o =>
                    // 1. Direkt bu öğrenciye atanmışsa (Harun'a özel)
                    o.OgrenciId == ogrenciId

                    // 2. VEYA (İstersen) Herkese açık/genel ödevleri de göster (OgrenciId boşsa)
                    || o.OgrenciId == null
                )
                .OrderByDescending(o => o.TeslimTarihi) // En yakın teslim tarihli en üstte
                .ToList();
        }
        // ------------------------------

        public IEnumerable<Odev> GetOdevlerForOgrenci(string ogrenciId)
        {
            return GetOdevlerByOgrenciId(ogrenciId);
        }
    }
}