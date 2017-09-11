using Common.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Common.Api
{
    public class Request : IRequest
    {
        private string baseAddress;
        private List<string> customHeaders;

        public Request(string baseAddress)
        {
            this.baseAddress = baseAddress;
            this.customHeaders = new List<string>();
        }

        public TResult Get<TResult>(string resource, NameValueCollection queryStringParameters = null)
        {
            var parameters = new QueryStringParameter().Add(queryStringParameters);
            return this.Get<TResult>(resource, parameters);
        }

        public TResult Get<TResult>(string resource, QueryStringParameter queryParameters = null)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.baseAddress);

                if (this.customHeaders.IsAny())
                    AddHeaderInClientRequest(this.customHeaders.ToArray(), client);

                resource = MakeResource(resource, queryParameters);


                var response = client.GetAsync(resource).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<TResult>(data);
                    return result;
                }

                return default(TResult);

            }
        }
        private string MakeResource(string resource, QueryStringParameter queryParameters)
        {
            if (queryParameters != null)
            {
                var queryStringUrl = queryParameters.Get().ToQueryString();
                resource = String.Concat(resource, queryStringUrl);
            }
            return resource;
        }
        private void AddHeaderInClientRequest(string[] headers, HttpClient client)
        {
            if (headers.IsAny())
            {
                foreach (var header in headers)
                {
                    var headerKey = header.Split(':')[0].Trim();
                    var headerValue = header.Split(':')[1].Trim();

                    if (headerKey == "Content-Type")
                        client.DefaultRequestHeaders
                            .Accept
                            .Add(new MediaTypeWithQualityHeaderValue(headerValue));
                    else
                        client.DefaultRequestHeaders.Add(headerKey, headerValue);
                }
            }
        }

    }
}
