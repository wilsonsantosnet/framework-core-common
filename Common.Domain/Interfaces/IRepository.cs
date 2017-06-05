using Common.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T Add(T entity);
        T Update(T entity);
        void Remove(T entity);
        PaginateResult<T> PagingAndDefineFields(FilterBase filters, IQueryable<T> queryFilter);

    }
}
