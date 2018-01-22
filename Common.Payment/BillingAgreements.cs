
using System;
using Common.Domain;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;

namespace Common.Payment
{
    public class BillingAgreements : PaymentBase
    {

        private string _billing_resource;

        public BillingAgreements(IRequest request):base(request)
        {
            this._billing_resource = "payments/billing-agreements";
        }

        public dynamic Create(dynamic data)
        {
            var result = this._request.Post<dynamic, dynamic>(this._billing_resource, data);
            return result;
        }


    }
}