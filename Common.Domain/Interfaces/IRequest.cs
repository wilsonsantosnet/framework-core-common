using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Common.Domain
{
    public interface IRequest
    {
        void SetAddress(string address);
        void AddHeaders(string item);
        void SetBearerToken(string accessToken);
        TResult Get<TResult>(string resource, NameValueCollection queryStringParameters = null);
        TResult Post<TResult, TModel>(string resource, TModel model);
    }
}
