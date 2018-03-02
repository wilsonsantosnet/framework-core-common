using Common.Domain;
using Common.Domain.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Payment
{
    public class PaymentBase
    {
        protected string _endpoint;
        protected string _authority_endpoint;
        protected string _client_Id;
        protected string _secret;
        protected IRequest _request;

        public PaymentBase(IRequest request, ConfigPaymentBase config)
        {
            this._endpoint = config.Endpoint;
            this._authority_endpoint = config.AuthorityEndpoint;
            this._client_Id = config.ClientId;
            this._secret = config.Secret;
            this._request = request;
            this._request.SetAddress(this._endpoint);
            this._request.AddHeaders("Content-Type", "application/json");
            this._request.SetBearerToken(this._request.GetAccessToken(this._authority_endpoint, this._client_Id, this._secret));
        }

    }
}
