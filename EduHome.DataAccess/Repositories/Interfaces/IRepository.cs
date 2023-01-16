using EduHome.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking=false);
        Task<T?> FindByIdAsync(int id);
        Task CreateAsync(T entity);

        void Update(T entity);
        void Delete(T entity);

        Task SaveAsync();

    }
}
