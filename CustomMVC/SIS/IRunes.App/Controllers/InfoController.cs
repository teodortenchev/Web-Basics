using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer;

namespace IRunes.App.Controllers
{
    class InfoController : Controller
    {
        public IHttpResponse About(IHttpRequest httpRequest)
        {
            return this.View();
        }
    }
}
