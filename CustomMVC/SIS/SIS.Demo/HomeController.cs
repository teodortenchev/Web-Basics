using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System.IO;

namespace SIS.Demo
{
    public class HomeController
    {
        private const string indexPath = @"E:\C# Fundamentals\Web Basics\code\Web-Basics\CustomMVC\SIS\SIS.Demo\Pages\index.html";
        public IHttpResponse Index(IHttpRequest request)
        {
            string content = File.ReadAllText(indexPath);

            return new HtmlResult(content, HttpResponseStatusCode.OK);
        }
    }
}
