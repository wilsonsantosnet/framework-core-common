﻿using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class ValidatorSpecification<T> : Dictionary<string, Rule<T>>
    {
        public ValidatorSpecification<T> WithRules(Func<KeyValuePair<string, Rule<T>>, bool> predicate)
        {
            var result = this.Where(predicate).ToDictionary(_ => _.Key, _ => _.Value);
            return result as ValidatorSpecification<T>;
        }

        public ValidationSpecificationResult Validate(T entity)
        {
            var isValid = true;
            var erros = new List<string>();
            foreach (var item in this)
            {
                if (!item.Value.GetSpecification().IsSatisfiedBy(entity))
                {
                    isValid = false;
                    erros.Add(item.Value.GetMessage());
                }
            }

            return new ValidationSpecificationResult {

                Errors = erros,
                IsValid = isValid,
            };
        }

    }
}
