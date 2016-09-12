using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace AltoWebClient
{
    public class CredentialWebClient : TokenAwareBetterWebClient
    {
        public object HttpUtility { get; private set; }

        public bool Login(string loginAddress, string email, string password, bool setBaseAddress = true)
        {
            WebRequest request = GetWebRequest(new Uri(loginAddress));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var loginData = new NameValueCollection();
            loginData.Add("email", email);
            loginData.Add("passwd", password);
            string content = String.Join("&",
                loginData.AllKeys.Select(a => a + "=" + loginData[a]));
            var buffer = Encoding.ASCII.GetBytes(content);
            request.ContentLength = buffer.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();
            var response = request.GetResponse();
            string res = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (StatusCode != HttpStatusCode.OK)
                return false;
            if (setBaseAddress)
            {
                BaseAddress = response.ResponseUri.ToString();
            }
            return true;
        }
    }
}
