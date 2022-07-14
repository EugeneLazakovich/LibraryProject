using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lesson1_DAL.Interfaces
{
    public interface IGenericRepository<T>
        where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> DeleteById(Guid id);
        Task<bool> Update(T item);
        Task<Guid> Add(T item);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllByPredicate(Expression<Func<T, bool>> predicate);
    }
}
