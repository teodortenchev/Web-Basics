using SIS.HTTP.Common;
using System;
using System.Collections;
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
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            return cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            //TO DO: Check if this is the correct behavior.
            if (cookies.ContainsKey(key))
            {
                return cookies[key];
            }

            return null;
        }

        public bool HasCookies()
        {
            return cookies.Count != 0;
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var cookie in cookies.Values)
            {
                sb.Append($"Set-Cookie: {cookie}").Append(GlobalConstants.HTTPNewLine);
            }

            return sb.ToString();
        }
    }
}
