using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Pandemica.Models;

namespace Pandemica.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Pandemica.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Appointment>("Appointments");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AppointmentsController : ODataController
    {
        private pandemicaDBEntities1 db = new pandemicaDBEntities1();

        // GET: odata/Appointments
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All)]
        public IQueryable<Appointment> GetAppointments()
        {
            return db.Appointments;
        }

        // GET: odata/Appointments(5)
        [EnableQuery]
        public SingleResult<Appointment> GetAppointment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Appointments.Where(appointment => appointment.AppointmentReferencenumber == key));
        }

        // PUT: odata/Appointments(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Appointment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment appointment = db.Appointments.Find(key);
            if (appointment == null)
            {
                return NotFound();
            }

            patch.Put(appointment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(appointment);
        }

        // POST: odata/Appointments
        public IHttpActionResult Post(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return Created(appointment);
        }

        // PATCH: odata/Appointments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Appointment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment appointment = db.Appointments.Find(key);
            if (appointment == null)
            {
                return NotFound();
            }

            patch.Patch(appointment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(appointment);
        }

        // DELETE: odata/Appointments(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Appointment appointment = db.Appointments.Find(key);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int key)
        {
            return db.Appointments.Count(e => e.AppointmentReferencenumber == key) > 0;
        }
    }
}
