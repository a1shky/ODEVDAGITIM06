using ODEVDAGITIM06.Data;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;

namespace ODEVDAGITIM06.Repositories
{
    // Bu sınıf, hem Repository<Ders>'ten temel metotları alır (Add, Delete...)
    // hem de IDersRepository sözleşmesini uygular.
    public class DersRepository : Repository<Ders>, IDersRepository
    {
        // Veritabanı bağlantısını (ApplicationDbContext) base sınıfa (Repository<T>)
        // göndermek için bu constructor'ı (yapıcı metot) yazmak zorundayız.
        public DersRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Gelecekte IDersRepository'ye özel bir metot eklersek,
        // o metodun kodunu buraya yazarız.
    }
}