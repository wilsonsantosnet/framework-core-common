using Common.Domain.Base;
using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IApplicationServiceBase<T>
    {
        T GetOne(FilterBase filters);

        SearchResult<T> GetByFilters(FilterBase filters);

        T Save(T entity, bool questionToContinue = false);

        T SavePartial(T entity, bool questionToContinue = false);

        IEnumerable<T> Save(IEnumerable<T> entitys);

        void Remove(T entity);

        ValidationWarning GetDomainWarning(FilterBase filters = null);

        ValidationConfirm GetDomainConfirm(FilterBase filters = null);

        ValidationSpecificationResult GetDomainValidation(FilterBase filters = null);


    }
}
