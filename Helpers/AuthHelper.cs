using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Helpers
{
    internal class AuthHelper
    {
        public static AuthenticationHeaderValue GetAuthHeader(string username, string password)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
