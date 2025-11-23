using ODEVDAGITIM06.Models;

namespace ODEVDAGITIM06.Repositories.Interfaces
{
    // Bu arayüz, IRepository'deki tüm temel metotları (Add, Delete, GetById...)
    // 'Ders' modeli için miras alır.
    public interface IDersRepository : IRepository<Ders>
    {
        // Şimdilik derse özel ekstra bir metoda ihtiyacımız yok.
        // İleride "Ders adına göre arama" gibi bir şey gerekirse buraya ekleriz.
    }
}