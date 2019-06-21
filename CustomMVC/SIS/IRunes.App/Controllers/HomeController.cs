using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
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
