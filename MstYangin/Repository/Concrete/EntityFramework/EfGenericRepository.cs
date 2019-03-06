using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MstYangin.Models;
using MstYangin.Repository.Abstract;




namespace MstYangin.Repository.Concrete.EntityFramework
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MstYanginContext context;
       
       // private UnitofWork.UnitofWork unitofWork;
        private readonly DbSet<T> _dbSet;
        
        public EfGenericRepository(MstYanginContext _context)
        {
            context = _context;
            _dbSet = context.Set<T>();
        }


     
        public void Add(T entity)
        {

            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            _dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public void Save()
        {
             context.SaveChanges();
        }
    }
}