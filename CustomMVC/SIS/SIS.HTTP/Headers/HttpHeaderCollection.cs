using System;
using System.Collections.Generic;

namespace SIS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            headers = new Dictionary<string, HttpHeader>();
        }
        public void AddHeader(HttpHeader header)
        {
            headers.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            return headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            if(!headers.ContainsKey(key))
            {
                return null;
            }

            return headers[key];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, headers);
        }
    }
}
