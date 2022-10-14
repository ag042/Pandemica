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
    builder.EntitySet<Clinic>("Clinics");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClinicsController : ODataController
    {
        private pandemicaDBEntities1 db = new pandemicaDBEntities1();


        


        // GET: odata/Clinics
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All)]
        public IQueryable<Clinic> GetClinics()
        {
            return db.Clinics;
        }

        // GET: odata/Clinics(5)
        [EnableQuery]
        public SingleResult<Clinic> GetClinic([FromODataUri] string key)
        {
            return SingleResult.Create(db.Clinics.Where(clinic => clinic.UniqueID == key));
        }

        // PUT: odata/Clinics(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<Clinic> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Clinic clinic = db.Clinics.Find(key);
            if (clinic == null)
            {
                return NotFound();
            }

            patch.Put(clinic);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(clinic);
        }

        // POST: odata/Clinics
        public IHttpActionResult Post(Clinic clinic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clinics.Add(clinic);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ClinicExists(clinic.UniqueID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(clinic);
        }

        // PATCH: odata/Clinics(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<Clinic> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Clinic clinic = db.Clinics.Find(key);
            if (clinic == null)
            {
                return NotFound();
            }

            patch.Patch(clinic);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(clinic);
        }

        // DELETE: odata/Clinics(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            Clinic clinic = db.Clinics.Find(key);
            if (clinic == null)
            {
                return NotFound();
            }

            db.Clinics.Remove(clinic);
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

        private bool ClinicExists(string key)
        {
            return db.Clinics.Count(e => e.UniqueID == key) > 0;
        }
    }
}
