using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System.Text;

namespace SIS.WebServer.Results
{
    public class TextResult : HttpResponse
    {
        private const string ContentType = "text/plain; charset=utf-8";

        public TextResult(string content, HttpResponseStatusCode responseStatusCode,
            string contentType = ContentType)
            : base(responseStatusCode)
        {
            Headers.AddHeader(new HttpHeader("Content-Type", contentType));
            Content = Encoding.UTF8.GetBytes(content);
        }

        public TextResult(byte[] content, HttpResponseStatusCode responseStatusCode,
            string contentType = ContentType)
            : base(responseStatusCode)
        {
            Content = content;
            Headers.AddHeader(new HttpHeader("Content-Type", contentType));
        }
    }
}
