using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer;
using SIS.WebServer.Result;

namespace IRunes.App.Controllers
{
    class InfoController : Controller
    {
        public ActionResult Json(IHttpRequest request)
        {
            return Json(new
            {
                Name = "test",
                Age = 25,
                Occupation = "Bezo"
            });
        }
        public IHttpResponse About(IHttpRequest httpRequest)
        {
            return this.View();
        }
    }
}
