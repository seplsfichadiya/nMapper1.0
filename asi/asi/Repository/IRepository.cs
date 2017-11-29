using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace asi.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Edit(T entity);
        void Insert(T entity);
        void Delete(T entity);
        void Save();
        void Dispose();
    }
}
