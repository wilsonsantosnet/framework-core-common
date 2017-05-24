using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Base
{
    public class DomainBase
    {
        protected ValidationSpecificationResult _validationResult;

        protected ValidationConfirm _validationConfirm;

        protected ValidationWarning _validationWarning;

        public virtual ValidationSpecificationResult GetDomainValidation(FilterBase filters = null)
        {
            return this._validationResult;
        }
        public virtual ValidationConfirm GetDomainConfirm(FilterBase filters = null)
        {
            return this._validationConfirm;
        }
        public virtual ValidationWarning GetDomainWarning(FilterBase filters = null)
        {
            return this._validationWarning;
        }

    }
}
