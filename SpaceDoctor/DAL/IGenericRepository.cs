using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpaceDoctor.DAL
{
    interface IGenericRepository<T>
    {
        IQueryable<T> Where(Expression<Func<T, bool>> where);

        void Delete(T entity);
        void Insert(T entity);
        void Update(T entity);
    }
}
