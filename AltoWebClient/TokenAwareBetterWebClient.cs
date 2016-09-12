using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AltoWebClient
{
    public class TokenAwareBetterWebClient : BetterWebClient
    {
        WebRequest _request = null;

        public List<string> QueryStrnigVaribalesToPreserve { get; set; }

        public TokenAwareBetterWebClient()
        {
            QueryStrnigVaribalesToPreserve = new List<string>();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            foreach (string variable in QueryString.Keys)
            {
                if (QueryStrnigVaribalesToPreserve.Contains(variable))
                {
                    values.Add(variable, QueryString[variable]);
                }
            }
            _request = base.GetWebRequest(address);

            var httpRequest = _request as HttpWebRequest;

            if (_request != null)
            {
                foreach (string variable in values.Keys)
                {
                    QueryString.Set(variable, values[variable]);
                }
            }

            return _request;
        }
    }
}
