using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class LibrariesController : ApiController
    {
        private WebApiDBEntities db = new WebApiDBEntities();

        // GET: api/Libraries
        public IQueryable<Library> GetLibraries()
        {
            return db.Libraries;
        }

        // GET: api/Libraries/5
        [ResponseType(typeof(Library))]
        public IHttpActionResult GetLibrary(int id)
        {
            Library library = db.Libraries.Find(id);
            if (library == null)
            {
                return NotFound();
            }

            return Ok(library);
        }

        // PUT: api/Libraries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLibrary(int id, Library library)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != library.ID)
            {
                return BadRequest();
            }

            db.Entry(library).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Libraries
        [ResponseType(typeof(Library))]
        public IHttpActionResult PostLibrary(Library library)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Libraries.Add(library);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = library.ID }, library);
        }

        // DELETE: api/Libraries/5
        [ResponseType(typeof(Library))]
        public IHttpActionResult DeleteLibrary(int id)
        {
            Library library = db.Libraries.Find(id);
            if (library == null)
            {
                return NotFound();
            }

            db.Libraries.Remove(library);
            db.SaveChanges();

            return Ok(library);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LibraryExists(int id)
        {
            return db.Libraries.Count(e => e.ID == id) > 0;
        }
    }
}