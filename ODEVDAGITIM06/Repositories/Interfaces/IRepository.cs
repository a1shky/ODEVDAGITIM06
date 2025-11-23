using System.Linq.Expressions;

namespace ODEVDAGITIM06.Repositories.Interfaces // Yeni adresi: Repositories'in içindeki Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}