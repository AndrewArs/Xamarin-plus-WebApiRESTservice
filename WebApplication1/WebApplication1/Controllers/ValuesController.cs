using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApplication1.Models;
using System.Web.Http.Description;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        private DateContext db = new DateContext();

        // GET: api/Values
        public IQueryable<Date> GetDates()
        {
            return db.Dates;
        }

        // GET: api/Values/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult GetDate(int id)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return NotFound();
            }

            return Ok(date);
        }

        // PUT: api/Values/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDate(int id, Date date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != date.Id)
            {
                return BadRequest();
            }

            db.Entry(date).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateExists(id))
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

        // POST: api/Values
        [HttpPost]
        public IHttpActionResult PostDate(string date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int _id = db.Dates.Max(d => d.Id) + 1;
            db.Dates.Add(new Models.Date() { _date = date, Id = _id});
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = _id }, date);
        }

        // DELETE: api/Values/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult DeleteDate(int id)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return NotFound();
            }

            db.Dates.Remove(date);
            db.SaveChanges();

            return Ok(date);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DateExists(int id)
        {
            return db.Dates.Count(e => e.Id == id) > 0;
        }
    }
}
