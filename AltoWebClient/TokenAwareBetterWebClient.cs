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
        public List<string> QueryStrnigVaribalesToPreserve { get; set; }

        public TokenAwareBetterWebClient()
        {
            QueryStrnigVaribalesToPreserve = new List<string>();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest _request = base.GetWebRequest(address);

            var httpRequest = _request as HttpWebRequest;

            if (_request != null)
            {
                foreach (string variable in QueryStrnigVaribalesToPreserve)
                {
                }
            }

            return _request;
        }
    }
}
