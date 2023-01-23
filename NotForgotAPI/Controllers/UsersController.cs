using NotForgotAPI.Entities;
using NotForgotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace NotForgotAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly NotForgotDatabaseEntities db = new NotForgotDatabaseEntities();

        [HttpPost, Route("api/Users/auth")]
        public IHttpActionResult Authorize([FromBody] AuthorizationRequest request)
        {
            var user = db.User.ToList().FirstOrDefault(x => x.Login == request.Login && x.Password == request.Password);
            if (user == null)
            {
                return BadRequest("Invalid Login or Password");
            }
            var accessToken = user.AccessToken.ToList().FirstOrDefault(x => x.EndTime > DateTime.Now);
            if (accessToken == null)
            {
                var currentTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm") + user.Login;
                var tokenValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(currentTime));
                var token = new AccessToken()
                {
                    User = user,
                    Value = tokenValue,
                    EndTime = DateTime.Now.AddHours(1)
                };
                accessToken = db.AccessToken.Add(token);
                db.SaveChanges();
            }

            var response = new AuthorizationResponse(accessToken.Value, new UserModel(user));
            return Ok(response);
        }

        [HttpPost, Route("api/Users/register")]
        public IHttpActionResult Register([FromBody] UserModel user)
        {
            try
            {
                var currUser = db.User.ToList().FirstOrDefault(x => x.Login == user.Login.Trim());
                if (currUser != null)
                {
                    return BadRequest("User with this login is exist");
                }
                var dbUser = new User()
                {
                    Id = user.Id,
                    Name = user.Name.Trim(),
                    Login = user.Login.Trim(),
                    Password = user.Password.Trim(),
                };
                db.User.Add(dbUser);
                db.SaveChanges();
                var model = new UserModel(db.User.ToList().FirstOrDefault(x => x.Login == user.Login.Trim()));
                return Ok(model);
            }
            catch
            {
                return BadRequest("Invalid data");
            }
        }

    }
}
