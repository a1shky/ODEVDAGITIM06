using ODEVDAGITIM06.Models;

namespace ODEVDAGITIM06.Repositories.Interfaces
{
    // IRepository'deki tüm temel metotları 'Teslim' için miras alır.
    public interface ITeslimRepository : IRepository<Teslim>
    {
        // YENİ EKLENDİ: Teslimleri, ilişkili olduğu 'Ödev' ve 'Ders' bilgisiyle birlikte getirecek bir metot.
        IEnumerable<Teslim> GetAllWithOdevDers();
    }
}