using IRunes.Models;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Result;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace IRunes.App.Controllers
{
    public abstract class BaseController
    {
        protected BaseController()
        {
            ViewData = new Dictionary<string, object>();
            ViewData["Header"] = File.ReadAllText("Views/Common/Header.html");
            ViewData["GuestHeader"] = File.ReadAllText("Views/Common/HeaderGuest.html");

            ViewData["Footer"] = File.ReadAllText("Views/Common/Footer.html");
            ViewData["Metadata"] = File.ReadAllText("Views/Common/Metadata.html");
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

        protected IHttpResponse View([CallerMemberName] string view = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = this.ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            return htmlResult;
        }

        protected IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected void SignIn(IHttpRequest httpRequest, User user)
        {
            httpRequest.Session.AddParameter("id", user.Id);
            httpRequest.Session.AddParameter("username", user.Username);
            httpRequest.Session.AddParameter("email", user.Email);
        }

        protected void SignOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

        }

        
    }
}
