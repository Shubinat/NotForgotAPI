using NotForgotAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NotForgotAPI.Utils
{
    public static class AuthUtils
    {
        public static AccessToken CheckAuth(this ApiController controller, Entities.NotForgotDatabaseEntities db)
        {
            var accessTokenScheme = controller.Request.Headers.Authorization?.Scheme;
            var accessTokenParametr = controller.Request.Headers.Authorization?.Parameter;

            if (accessTokenScheme != "Bearer")
            {
                return null;
            }


            var dbToken = db.AccessToken.ToList().FirstOrDefault(x => x.Value == accessTokenParametr);
            if (dbToken.EndTime <= DateTime.Now)
            {
                db.AccessToken.Remove(dbToken);
                db.SaveChanges();
                return null;
            }
            return dbToken;
        }
    }
}