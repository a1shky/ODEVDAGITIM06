using System;
using System.Collections.Generic; // İŞTE EKSİK OLAN BU! Bunu ekledik düzeldi.
using System.Linq;
using System.Linq.Expressions;

namespace ODEVDAGITIM06.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id); // Bunu 'int id' yaptık, kullanımı çok daha kolay olacak.
    }
}