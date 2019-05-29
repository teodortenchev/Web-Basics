using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            cookies = new Dictionary<string, HttpCookie>();
        }

        public void AddCookie(HttpCookie cookie)
        {
            throw new NotImplementedException();
        }

        public bool ContainsCookie(string key)
        {
            throw new NotImplementedException();
        }

        public HttpCookie GetCookie(string key)
        {
            throw new NotImplementedException();
        }

        public bool HasCookies()
        {
            throw new NotImplementedException();
        }
    }
}
