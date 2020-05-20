using System;
using System.Collections.Generic;
using System.Text;

namespace TechShare.DAL
{
    public interface IGenericRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetByID(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
