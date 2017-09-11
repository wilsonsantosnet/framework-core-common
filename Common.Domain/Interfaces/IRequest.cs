using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Common.Domain
{
    public interface IRequest
    {
        TResult Get<TResult>(string resource, NameValueCollection queryStringParameters = null);
    }
}
