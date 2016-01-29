using SoundConnect.Survey.Core.Repositories;
using SoundConnect.Survey.Data;
using SoundConnect.Survey.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace SoundConnect.Survey.DBContext.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        protected Database _dataBase;
        protected IDbSet<T> _dbset;
        public IDataContext _context;
        public DbContextTransaction _dbContextTransaction;

        public IDataContext Context
        {
            get { return _context; }
        }

        public DbContext DBContext
        {
            get { return (DbContext)_context; }
        }

        public DbContextTransaction ContextTransaction
        {
            get { return _dbContextTransaction; }
        }


        public EntityRepository(IDataContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContextTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            _dbContextTransaction.Rollback();
        }

        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var ent = _dbset.Add(entity);
            _context.SaveChanges();
            return ent;
        }

        public virtual T Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbset.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbset.AsQueryable<T>();
        }

        public virtual T GetById(int id)
        {
            return _dbset.Find(id);
        }
    }
}
