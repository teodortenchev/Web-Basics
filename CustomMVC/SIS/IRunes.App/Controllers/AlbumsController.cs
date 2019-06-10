using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
    public class AlbumsController : BaseController
    {
        public IHttpResponse AlbumView(IHttpRequest httpRequest)
        {
            if(IsLoggedIn(httpRequest))
            {
                return View("All");
            }

            return Redirect("/Users/Login");
        }
    }
}
