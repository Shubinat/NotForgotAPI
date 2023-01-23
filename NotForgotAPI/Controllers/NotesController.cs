using NotForgotAPI.Entities;
using NotForgotAPI.Models;
using NotForgotAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NotForgotAPI.Controllers
{
    public class NotesController : ApiController
    {
        private readonly NotForgotDatabaseEntities db = new NotForgotDatabaseEntities();
        // GET: api/Notes
        public IHttpActionResult Get()
        {
            var dbToken = this.CheckAuth(db);
            if (dbToken == null)
                return Unauthorized();

            var notes = db.Note.ToList().Where(x => x.UserId == dbToken.UserId).ToList();

            return Ok(notes.ConvertAll(note => new NoteModel(note)));
        }

        // GET: api/Notes/5
        public IHttpActionResult Get(int id)
        {
            var dbToken = this.CheckAuth(db);
            if (dbToken == null)
                return Unauthorized();

            var note = db.Note.FirstOrDefault(x => x.Id == id);
            if (note == null)
                return NotFound();
            if (note.UserId == dbToken.UserId)
                return Ok(new NoteModel(note));
            else
                return Unauthorized();
        }

        // POST: api/Notes
        public IHttpActionResult Post([FromBody] NoteModel note)
        {
            var dbToken = this.CheckAuth(db);
            if (dbToken == null)
                return Unauthorized();

            try
            {
                var dbNote = new Note()
                {
                    Title = note.Title,
                    Description = note.Description,
                    CreationDate = DateTime.Now,
                    CompletionDate = DateTime.Parse(note.CompletionDate),
                    IsCompleted = note.IsCompleted,
                    CategoryId = note.CategoryId,
                    UserId = dbToken.UserId,
                    Priority = note.Priority,
                };
                var noteModel = db.Note.Add(dbNote);
                db.SaveChanges();
                return Ok(new NoteModel(db.Note.ToList().First(x => x == noteModel)));
            }
            catch
            {
                return BadRequest("Invalid data");
            }
        }

        // PUT: api/Notes/5
        public IHttpActionResult Put(int id, [FromBody]NoteModel note)
        {
            var dbToken = this.CheckAuth(db);
            if (dbToken == null)
                return Unauthorized();

            var dbNote = db.Note.FirstOrDefault(x => x.Id == id);
            if (dbNote == null)
                return NotFound();
            if (dbNote.UserId != dbToken.UserId)
                return Unauthorized();

            try
            {
                dbNote.Title = note.Title;
                dbNote.Description = note.Description;
                dbNote.CompletionDate = DateTime.Parse(note.CompletionDate);
                dbNote.IsCompleted = note.IsCompleted;
                dbNote.CategoryId = note.CategoryId;
                dbNote.Priority = note.Priority;

                db.SaveChanges();
                return Ok(new NoteModel(dbNote));
            }
            catch
            {
                return BadRequest("Invalid data");
            }
        }

        // DELETE: api/Notes/5
        public IHttpActionResult Delete(int id)
        {
            var dbToken = this.CheckAuth(db);
            if (dbToken == null)
                return Unauthorized();

            var note = db.Note.FirstOrDefault(x => x.Id == id);
            if (note == null)
                return NotFound();
            if (note.UserId != dbToken.UserId)
                return Unauthorized();

            try
            {
                db.Note.Remove(note);
                db.SaveChanges();
                return Ok(new NoteModel(note));
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
