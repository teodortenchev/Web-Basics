using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                string username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                string password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();

                User userFromDb = context.Users.FirstOrDefault(user => (user.Username == username
                || user.Email == username) && user.Password == HashPassword(password));

                if (userFromDb == null)
                {
                    return Redirect("/Users/Login");
                    
                }

                this.SignIn(httpRequest, userFromDb.Id, userFromDb.Username, userFromDb.Email);
                
            }

            return Redirect("/");
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                string username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                string password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();
                string passwordConfirmation = ((ISet<string>)httpRequest.FormData["confirmPassword"]).FirstOrDefault();
                string email = ((ISet<string>)httpRequest.FormData["email"]).FirstOrDefault();

                if (password != passwordConfirmation)
                {
                    return Redirect("/Users/Register");
                }

                User user = new User
                {
                    Username = WebUtility.UrlDecode(username),
                    Password = HashPassword(password),
                    Email = WebUtility.UrlDecode(email)
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return Redirect("/Users/Login");

        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            SignOut(httpRequest);
            return Redirect("/");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
