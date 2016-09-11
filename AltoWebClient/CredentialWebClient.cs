using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AltoWebClient
{
    public class CredentialWebClient : TokenAwareBetterWebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest _request = base.GetWebRequest(address);

            var httpRequest = _request as HttpWebRequest;

            if (_request != null)
            {
                
            }

            return _request;
        }
    }
}
