using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System.IO;
using System.Threading;

namespace SIS.Demo
{
    public class HomeController
    {
        private const string indexPath = @"E:\C# Fundamentals\Web Basics\code\Web-Basics\CustomMVC\SIS\SIS.Demo\Pages\index.html";
        public IHttpResponse Index(IHttpRequest request)
        {
            string content = File.ReadAllText(indexPath);

            HtmlResult htmlResult = new HtmlResult(content, HttpResponseStatusCode.OK);
            htmlResult.Cookies.AddCookie(new HTTP.Cookies.HttpCookie("ted", "statusNOW"));
            return htmlResult;
        }
    }
}
