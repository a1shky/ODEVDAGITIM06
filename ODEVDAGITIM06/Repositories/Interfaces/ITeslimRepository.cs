using ODEVDAGITIM06.Models;
using System.Collections.Generic;

namespace ODEVDAGITIM06.Repositories.Interfaces
{
    public interface ITeslimRepository : IRepository<Teslim>
    {
        // Listeleme sayfasında kullandığın metot
        IEnumerable<Teslim> GetAllWithOdevDers();

        // YENİ EKLEDİĞİMİZ METOT: Detaylarıyla (Öğrenci+Ödev) getirme
        Teslim GetByIdWithDetails(int id);
    }
}