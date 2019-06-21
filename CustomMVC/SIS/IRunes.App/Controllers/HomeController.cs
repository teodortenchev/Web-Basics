using SIS.HTTP.Requests;
using SIS.HTTP.Responses;

namespace IRunes.App.Controllers
{
    public class HomeController : BaseController
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
