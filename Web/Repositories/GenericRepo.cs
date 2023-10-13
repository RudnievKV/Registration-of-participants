using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly MyDBContext _context;
        public GenericRepo(MyDBContext dbContext)
        {
            _context = dbContext;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void CreateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}