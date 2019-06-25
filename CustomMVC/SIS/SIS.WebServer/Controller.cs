using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Result;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace SIS.WebServer
{
    public abstract class Controller
    {
        protected Controller()
        {
            ViewData = new Dictionary<string, object>();
            ViewData["Header"] = System.IO.File.ReadAllText("Views/Common/Header.html");
            ViewData["GuestHeader"] = System.IO.File.ReadAllText("Views/Common/HeaderGuest.html");

            ViewData["Footer"] = System.IO.File.ReadAllText("Views/Common/Footer.html");
            ViewData["Metadata"] = System.IO.File.ReadAllText("Views/Common/Metadata.html");
        }

        protected Dictionary<string, object> ViewData;

        
        private string ParseTemplate(string viewContent)
        {
            foreach (var param in this.ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}", 
                    param.Value.ToString());
            }

            return viewContent;
        }

        
        protected bool IsLoggedIn(IHttpRequest httpRequest)

        {
            return httpRequest.Session.ContainsParameter("username");
        }

        protected ActionResult View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = System.IO.File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = this.ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            return htmlResult;
        }

        protected ActionResult Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected ActionResult Xml(object param)
        {
            return null;
        }

        protected ActionResult Json(object param)
        {
            return null;
        }

        protected ActionResult File()
        {
            return null;
        }

        protected void SignIn(IHttpRequest httpRequest, string id, string username, string email)
        {
            httpRequest.Session.AddParameter("id", id);
            httpRequest.Session.AddParameter("username", username);
            httpRequest.Session.AddParameter("email", email);
        }

        protected void SignOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

        }



        
    }
}
