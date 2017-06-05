using Common.Domain.Base;
using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IServiceBase<T,TF>
    {
        T GetOne(TF filters);

        IEnumerable<T> GetByFilters(TF filters);

        PaginateResult<T> GetByFiltersPaging(TF filters);

        T SavePartial(T entity,  bool questionToContinue = false);

        T Save(T entity, bool questionToContinue = false);

        IEnumerable<T> Save(IEnumerable<T> entitys);

        void Remove(T entity);

        Summary GetSummary(PaginateResult<T> paginateResult);

        ValidationConfirm GetDomainConfirm(FilterBase filters = null);

        ValidationWarning GetDomainWarning(FilterBase filters = null);

        ValidationSpecificationResult GetDomainValidation(FilterBase filters = null);

        void AddDomainValidation(IEnumerable<string> errors);

        void AddDomainValidation(string error);

        void SetTagNameCache(string _tagNameCache);

        string GetTagNameCache();

    }
}
