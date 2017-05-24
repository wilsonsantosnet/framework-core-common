using Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Model
{

    public class CurrentUser
    {
 
        private string _token;


     
        public void Init(string token, Dictionary<string, object> claims)
        {
            this._token = token;
            this._claims = claims;
        }

        public int UserId { get; set; }

        public object UserInfo { get; set; }

        private Dictionary<string, object> _claims;

        public string GetToken()
        {
            return this._token;
        }

        public Dictionary<string, object> GetClaims()
        {
            return this._claims;
        }

        public bool ExistsSubscriberId()
        {
            return this._claims.Where(_ => _.Key.ToLower() == "subscriberid").IsAny();
        }
        public TS GetSubscriberId<TS>()
        {
            if (this.ExistsSubscriberId())
            {
                var subscriberId = this._claims.Where(_ => _.Key.ToLower() == "subscriberid").SingleOrDefault().Value;
                return (TS)Convert.ChangeType(subscriberId, typeof(TS));
            }
            return default(TS);
        }

        public T GetUserInfo<T>() where T : class
        {
            var result = (T)UserInfo as T;
            return result;
        }

    }
}
