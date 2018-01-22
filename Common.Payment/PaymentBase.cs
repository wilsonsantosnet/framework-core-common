using Common.Domain;
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

        public PaymentBase(IRequest request)
        {
            this._endpoint = "https://api.sandbox.paypal.com/v1/";
            this._authority_endpoint = "https://api.sandbox.paypal.com/v1/oauth2/token";
            this._client_Id = "AZ-CV9-rWilSbHxUlSSwL5T0N7sJhtB-c6X1-L4wW6uBzgwCT4Kw0AuOmUrT42aiN8dob9D7oUghn8hd";
            this._secret = "EJuyh7jEqxLCv2jmaUEpWESFaZWv6ZyRqhRan5B1lzGJ6Yp03LkrbiUitBk1Xyz9_E2xrxKcgHjnUKhD";

            this._request = request;
            this._request.SetAddress(this._endpoint);
            this._request.AddHeaders("Content-Type", "application/json");
            this._request.SetBearerToken(this._request.GetAccessToken(this._authority_endpoint, this._client_Id, this._secret));
        }

    }
}
