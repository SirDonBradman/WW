using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;
using WebApplication1.Utilities;

namespace WebApplication1.Controllers.API
{
    public class GuestsController : ApiController
    {
        private wdbEntities db = new wdbEntities();

        // GET: api/Guests
        public IEnumerable<guest> Getguests()
        {
            return db.guests.ToList();
        }

        [ResponseType(typeof(guest))]
        public async Task<IHttpActionResult> Postguest(guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestInDB = db.guests.FirstOrDefault(g => g.email.Equals(guest.email));
            if (guestInDB == null)
            {
                guest.id = Guid.NewGuid().ToString("N");
                db.guests.Add(guest);
            }
            else
            {
                guestInDB.numberOfGuests = guest.numberOfGuests;
                guestInDB.coming = guest.coming;
                guestInDB.name = guest.name;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (guestExists(guest.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //send email
            Email email = new Email();
            await email.SendAsync(guest.email, null, "Wedding Invitation Sanmathi and Shivam", "" +
                "Wedding Invitation");

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool guestExists(string id)
        {
            return db.guests.Count(e => e.id == id) > 0;
        }
    }
}