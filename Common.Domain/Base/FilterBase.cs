﻿using Common.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Base
{
    public class FilterBase
    {

        public FilterBase()
        {
            this.PageIndex = 0;
            this.PageSize = 50;
        }

        public int PageSkipped
        {
            get
            {
                return (this.PageIndex > 0 ? this.PageIndex - 1 : 0) * this.PageSize;
            }
        }

        public FilterBehavior FilterBehavior { get; set; }
        public bool IsPagination { get; set; }
        public string QueryOptimizerBehavior { get; set; }
        public bool IsOrderByDomain { get; set; }
        public string[] OrderFields { get; set; }
        public OrderByType orderByType { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public bool IsOnlySummary { get; set; }

        public bool ByCache { get; set; }

        public TimeSpan CacheExpiresTime { get; set; }

        public string SummaryBehavior { get; set; }

        public string FilterKey { get; set; }
    }
}
