using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechShare.Entity;

namespace TechShare.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TechShareDBContext _context;
        protected readonly DbSet<T> _set;
        public GenericRepository(TechShareDBContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _set.ToList();          
        }

        public T GetByID(object id)
        {
            return _set.Find(id);
        }

        public void Insert(T entity)
        {
            _set.Add(entity);
        }

        public void Update(T entity)
        {
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T entity = _set.Find(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _set.Attach(entity);
            }
            _set.Remove(entity);
        }
    }
}
