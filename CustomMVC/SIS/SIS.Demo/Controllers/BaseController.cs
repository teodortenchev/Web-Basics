using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System.IO;
using System.Runtime.CompilerServices;

namespace SIS.Demo.Controllers
{
    public abstract class BaseController
    {
        protected IHttpRequest HttpRequest { get; set; }
        public IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.OK);

            return htmlResult;
        }

        public IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

        private bool IsLoggedIn()
        {
            return HttpRequest.Session.ContainsParameter("username");
        }
        private string ParseTemplate(string viewContent)
        {

            if (IsLoggedIn())
            {
                return viewContent.Replace("@Model.HelloMessage", $"Hello, {HttpRequest.Session.GetParameter("username")}");
            }
            else
            {
                return viewContent.Replace("@Model.HelloMessage", "Hello from the other side");
            }


        }
    }
}
