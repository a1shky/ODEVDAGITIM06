using System.Collections.Generic;
using ODEVDAGITIM06.Models;

namespace ODEVDAGITIM06.Repositories.Interfaces
{
    public interface IOdevRepository : IRepository<Odev>
    {
        // 1. Sadece Ders bilgisini getiren metot
        IEnumerable<Odev> GetAllWithDers();

        // 2. Hem Ders hem de Öğrenci bilgisini getiren metot (Admin Listesi İçin)
        IEnumerable<Odev> GetAllWithDersAndOgrenci();

        // 3. Sadece belirli bir öğrenciye ait ödevleri getiren metot (Eski yöntem)
        IEnumerable<Odev> GetOdevlerByOgrenciId(string ogrenciId);

        // --- İŞTE EKSİK OLAN BU SATIR ---
        // 4. Öğrencinin ID'si eşleşenleri VEYA Herkese Açık olanları getiren metot
        IEnumerable<Odev> GetOdevlerForOgrenci(string ogrenciId);
    }
}