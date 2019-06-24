using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer;
using SIS.WebServer.Attributes;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet(Url = "/")]
        public IHttpResponse IndexShash(IHttpRequest httpRequest)
        {
            return Index(httpRequest);
        }
        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            if(IsLoggedIn(httpRequest))
            {
                ViewData["Username"] = httpRequest.Session.GetParameter("username");
                return View("Home");
            }

            return View();
        }
    }
}
