using System;
using System.Collections.Generic;
using System.Text;

namespace TechShare.DAL
{
    public interface IUnitOfWork:IDisposable
    {
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
