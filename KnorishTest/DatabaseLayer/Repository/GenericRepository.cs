using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;

namespace DatabaseLayer
{
    public class DbRepository<T> : IDbRepository<T> where T : class
    {
        BoatContext dbContext;
        public DbRepository()
        {
            dbContext = new BoatContext();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> expression)
        {
            return this.dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> FindAll()
        {
            return this.dbContext.Set<T>().AsNoTracking();
        }

        public T Save(T _entity)
        {
            dbContext.Set<T>().Add(_entity);
            dbContext.SaveChanges();
            return _entity;
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }

        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //// Protected implementation of Dispose pattern.
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposed)
        //        return;

        //    if (disposing)
        //    {
        //        handle.Dispose();
        //        this.dbContext.Dispose();
        //    }

        //    disposed = true;
        //}
    }

    public interface IDbRepository<T>
    {
        T Save(T _entity);
        IQueryable<T> FindAll();
        IQueryable<T> Filter(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void Delete(T entity);
    }
}
