using IRunes.Data;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System.Linq;
using IRunes.App.Extensions;
using System.Collections.Generic;
using IRunes.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace IRunes.App.Controllers
{
    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/Users/Login");
            }


            string albumId = httpRequest.QueryData["albumId"].ToString();
            ViewData["AlbumId"] = albumId;

            return View();

        }

        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                string albumId = httpRequest.QueryData["albumId"].ToString();

                string name = ((ISet<string>)httpRequest.FormData["name"]).FirstOrDefault();
                string link = ((ISet<string>)httpRequest.FormData["link"]).FirstOrDefault();
                decimal price = decimal.Parse(((ISet<string>)httpRequest.FormData["price"]).FirstOrDefault());

                Album album = context.Albums.Include(a => a.Tracks).FirstOrDefault(x => x.Id == albumId);

                if (album == null)
                {
                    return Redirect("/Albums/All");
                }

                ViewData["AlbumId"] = album.Id;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)
                    || string.IsNullOrWhiteSpace(link) || string.IsNullOrEmpty(link))
                {
                    return Redirect("/Tracks/Create?albumId=@Model.AlbumId");
                }


                Track track = new Track
                {
                    Name = name,
                    Link = link,
                    Price = price
                };

                album.Tracks.Add(track);
                decimal sum = album.Tracks.Select(x => x.Price).Sum();
                album.Price = sum * 87/100;
                context.Update(album);
                context.SaveChanges();

                return Redirect($"/Albums/Details?id={album.Id}");
            }





        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!IsLoggedIn(httpRequest))
            {
                return Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["albumId"].ToString();
            string trackId = httpRequest.QueryData["trackId"].ToString();

            using (var context = new RunesDbContext())
            {
                Album album = context.Albums.Include(a => a.Tracks).FirstOrDefault(a => a.Id == albumId);
                Track track = album.Tracks.FirstOrDefault(t => t.Id == trackId);


                if (album == null || track == null)
                {
                    return Redirect("Albums/All");
                }



                ViewData["Track"] = track.ToHtmlDetails();
                ViewData["SongName"] = WebUtility.UrlDecode(track.Name);
                ViewData["SongPrice"] = $"{track.Price:F2}";

                ViewData["VideoUrl"] = track.Link;
                ViewData["AlbumId"] = album.Id;

                return View();

            }
        }
    }
}
