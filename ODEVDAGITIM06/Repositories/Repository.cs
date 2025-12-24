using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Data;
using ODEVDAGITIM06.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ODEVDAGITIM06.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            _context.SaveChanges();
        }

        // --- İŞTE BURASI ÇOK ÖNEMLİ ---
        // IRepository'de "int id" demiştik, burada da "int id" olmak ZORUNDA.
        public void Delete(int id)
        {
            T entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}