using EduHome.Core.Contexts;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {

        public readonly AppDbContext _context;
        public readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table=_context.Set<T>();
                
        }


        public IQueryable<T> FindAll()
        {
           return _table.AsQueryable();
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return _table.Find(id);

        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,bool isTracking=false)
        {
            if(isTracking)
                return _table.Where(expression);
            return _table.Where(expression).AsNoTracking();

        }



        public async Task CreateAsync(T entity)
        {
           await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
          _table.Remove(entity);
        }

       
        public void Update(T entity)
        {
           _table.Update(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
