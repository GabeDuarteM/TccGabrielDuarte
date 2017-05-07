using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase
    {
        ICollection<T> GetAll();
        ICollection<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();
    }
}
