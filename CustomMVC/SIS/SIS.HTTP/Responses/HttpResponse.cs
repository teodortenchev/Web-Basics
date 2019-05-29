using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            Headers = new HttpHeaderCollection();
            Content = new byte[0];
            Cookies = new HttpCookieCollection();
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            StatusCode = statusCode;
        }

        public IHttpCookieCollection Cookies { get; private set; }
        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            Headers.AddHeader(header);
        }
        
        public byte[] GetBytes()
        {
            byte[] httpResponseBytesWithoutBody = Encoding.UTF8.GetBytes(this.ToString());
            byte[] httpResponseBytesWithBody = new byte[httpResponseBytesWithoutBody.Length + Content.Length];

            for (int i = 0; i < httpResponseBytesWithoutBody.Length; i++)
            {
                httpResponseBytesWithBody[i] = httpResponseBytesWithoutBody[i];
            }

            for (int i = 0; i < httpResponseBytesWithBody.Length - httpResponseBytesWithoutBody.Length; i++)
            {
                httpResponseBytesWithBody[i + httpResponseBytesWithoutBody.Length] = Content[i];
            }

            return httpResponseBytesWithBody;
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            Cookies.AddCookie(cookie);
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result
                .Append($"{GlobalConstants.HttpOneProtocolFragment} {(int)StatusCode} {StatusCode.ToString()}")
                .Append(GlobalConstants.HTTPNewLine)
                .Append(Headers)
                .Append(GlobalConstants.HTTPNewLine);

            if(Cookies.HasCookies())
            {
                result.Append($"{Cookies}").Append(GlobalConstants.HTTPNewLine);
            }


            result.Append(GlobalConstants.HTTPNewLine);

            return result.ToString();
        }
    }
}
