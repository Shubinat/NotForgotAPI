using NotForgotAPI.Entities;
using NotForgotAPI.Models;
using NotForgotAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace NotForgotAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        public NotForgotDatabaseEntities db = new NotForgotDatabaseEntities();
        // GET: api/Categories
        public IHttpActionResult Get()
        {
            var dbToken = this.CheckAuth(db);
            if (dbToken == null)
                return Unauthorized();

            var categories = db.Category.ToList().Where(x => x.UserId == dbToken.UserId).ToList();
            return Ok(categories.ConvertAll(category => new CategoryModel(category)));
        }
        // POST: api/Categories
        public IHttpActionResult Post([FromBody] CategoryModel category)
        {
            var dbToken = this.CheckAuth(db);
            if (dbToken == null)
                return Unauthorized();

            try
            {
                if (db.Category.ToList().FirstOrDefault(x => x.Name == category.Name.Trim()) != null)
                    return BadRequest("Category with this name is exist");

                var dbCategory = new Category()
                {
                    User = dbToken.User,
                    Name = category.Name.Trim(),
                };
                db.Category.Add(dbCategory);
                db.SaveChanges();

                return Ok(new CategoryModel(db.Category.ToList().First(x => x.Name == category.Name)));
            }
            catch
            {
                return BadRequest("Invalid data");
            }
        }

    }
}
