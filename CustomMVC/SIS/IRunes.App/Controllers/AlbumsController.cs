using IRunes.Data;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System.Linq;
using IRunes.App.Extensions;
using System.Collections.Generic;
using IRunes.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using SIS.WebServer;
using SIS.WebServer.Attributes;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        public IHttpResponse All(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                ICollection<Album> allAlbums = context.Albums.ToList();

                if (allAlbums.Count == 0)
                {
                    ViewData["Albums"] = "There are currently no albums";
                }
                else
                {
                    ViewData["Albums"] = string.Join("<br />", allAlbums.Select(album => album.ToHtmlAll()));
                }


                return View();
            }


        }

        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/Users/Login");
            }

            return View();

        }

        [HttpPost(ActionName = "Create")]
        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                string name = ((ISet<string>)httpRequest.FormData["name"]).FirstOrDefault();
                string cover = ((ISet<string>)httpRequest.FormData["cover"]).FirstOrDefault();

                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                {
                    return Redirect("/Albums/All");
                }

                if (string.IsNullOrEmpty(cover) || string.IsNullOrWhiteSpace(cover))
                {
                    cover = "https://dummyimage.com/600x400/000/ffffff.jpg&text=Missing+Album+Art";
                }

                Album album = new Album
                {
                    Name = WebUtility.UrlDecode(name),
                    Cover = cover
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }

            return Redirect("/Albums/All");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["id"].ToString();

            using (var context = new RunesDbContext())
            {
                Album album = context.Albums.Include(a => a.Tracks).FirstOrDefault(a => a.Id == albumId);


                if (album == null)
                {
                    Redirect("Albums/All");
                }



                ViewData["Album"] = album.ToHtmlDetails();
                ViewData["Cover"] = WebUtility.UrlDecode(album.Cover);
                ViewData["Name"] = WebUtility.UrlDecode(album.Name);
                ViewData["Price"] = $"{album.Price:F2}";
                ViewData["Id"] = album.Id;

                return View();

            }

        }
    }
}
