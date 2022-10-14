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
    builder.EntitySet<VaccinationCenter>("VaccinationCenters");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class VaccinationCentersController : ODataController
    {
        private pandemicaDBEntities1 db = new pandemicaDBEntities1();

        // GET: odata/VaccinationCenters
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All)]
        public IQueryable<VaccinationCenter> GetVaccinationCenters()
        {
            return db.VaccinationCenters;
        }

        // GET: odata/VaccinationCenters(5)
        [EnableQuery]
        public SingleResult<VaccinationCenter> GetVaccinationCenter([FromODataUri] string key)
        {
            return SingleResult.Create(db.VaccinationCenters.Where(vaccinationCenter => vaccinationCenter.UniqueID == key));
        }

        // PUT: odata/VaccinationCenters(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<VaccinationCenter> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VaccinationCenter vaccinationCenter = db.VaccinationCenters.Find(key);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            patch.Put(vaccinationCenter);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationCenterExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vaccinationCenter);
        }

        // POST: odata/VaccinationCenters
        public IHttpActionResult Post(VaccinationCenter vaccinationCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VaccinationCenters.Add(vaccinationCenter);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VaccinationCenterExists(vaccinationCenter.UniqueID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(vaccinationCenter);
        }

        // PATCH: odata/VaccinationCenters(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<VaccinationCenter> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VaccinationCenter vaccinationCenter = db.VaccinationCenters.Find(key);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            patch.Patch(vaccinationCenter);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationCenterExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vaccinationCenter);
        }

        // DELETE: odata/VaccinationCenters(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            VaccinationCenter vaccinationCenter = db.VaccinationCenters.Find(key);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            db.VaccinationCenters.Remove(vaccinationCenter);
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

        private bool VaccinationCenterExists(string key)
        {
            return db.VaccinationCenters.Count(e => e.UniqueID == key) > 0;
        }
    }
}
