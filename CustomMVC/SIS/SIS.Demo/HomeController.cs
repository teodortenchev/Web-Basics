using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;

namespace SIS.Demo
{
    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            string content = "<h1>I pray to the powers to be that this works after so many days of coding!</h1>";

            return new HtmlResult(content, HttpResponseStatusCode.OK);
        }
    }
}
