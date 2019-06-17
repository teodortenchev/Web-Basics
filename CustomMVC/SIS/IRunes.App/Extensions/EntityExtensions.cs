using IRunes.Data;
using IRunes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IRunes.App.Extensions
{
    public static class EntityExtensions
    {
        public static string ToHtmlAll(this Album album)
        {
            return $"<a class=\"font-weight-bold\" href=\"/Albums/Details?id={album.Id}\">{album.Name}</a>";
        }

        public static string ToHtmlDetails(this Album album)
        {
            string albumDetails = File.ReadAllText("Views/Albums/AlbumDetails.html");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(albumDetails);

            

            if (GetTracks(album).Count == 0)
            {
                sb.AppendLine($"<p>The album is empty. Mind adding some songs?</p>");
            }
            else
            {
                sb.AppendLine($"<ol>");
                foreach (var track in album.Tracks)
                {
                    sb.AppendLine($"<li>{track.ToHtmlAll(album.Id)}</li>");
                }
                sb.AppendLine($"</ol>");
            }

            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return sb.ToString();
        }

        public static string ToHtmlAll(this Track track, string albumId)
        {
            return $"<strong><a href=\"/Tracks/Details?albumId={albumId}&trackId={track.Id}\">{WebUtility.UrlDecode(track.Name)}</a></strong>";     
        }

        public static string ToHtmlDetails(this Track track)
        {
            string trackDetails = File.ReadAllText("Views/Tracks/TrackDetails.html");
            
            return trackDetails;
        }

        private static List<Track> GetTracks(Album album)
        {
            return album.Tracks.ToList();
        }
    }
}
