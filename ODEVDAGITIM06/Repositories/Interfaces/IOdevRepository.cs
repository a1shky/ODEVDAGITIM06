using ODEVDAGITIM06.Models;

namespace ODEVDAGITIM06.Repositories.Interfaces
{
    // IRepository'deki tüm temel metotları 'Odev' için miras alır.
    public interface IOdevRepository : IRepository<Odev>
    {
        // YENİ EKLENDİ: Ödevleri, ilişkili olduğu 'Ders' bilgisiyle birlikte getirecek bir metot.
        IEnumerable<Odev> GetAllWithDers();
    }
}