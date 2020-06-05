using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using TechShare.Entity;

namespace TechShare.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechShareDBContext _context;
        private IDbContextTransaction _transaction;
        public PostRepository PostRepository { get; private set; }
        public CategoryRepository CategoryRepository { get; private set; }
        public PostCategoryRepos PostCategoryRepos { get; private set; }
        public UnitOfWork(TechShareDBContext context)
        {
            _context = context;
            PostRepository = new PostRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            PostCategoryRepos = new PostCategoryRepos(_context);
        }
        public void CreateTransaction()
        {
            _transaction = _context.Database.BeginTransaction();            
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }        
    }
}
