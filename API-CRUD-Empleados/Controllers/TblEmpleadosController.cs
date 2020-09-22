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
using API_CRUD_Empleados.Models;
using System.Web.Http.Cors;

namespace API_CRUD_Empleados.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TblEmpleadosController : ApiController
    {
        private crudEmpleadosEntities db = new crudEmpleadosEntities();

        // GET: api/TblEmpleados
        public IQueryable<TblEmpleados> GetTblEmpleados()
        {
            return db.TblEmpleados;
        }

        // GET: api/TblEmpleados/5
        [ResponseType(typeof(TblEmpleados))]
        public IHttpActionResult GetTblEmpleados(decimal id)
        {
            TblEmpleados tblEmpleados = db.TblEmpleados.Find(id);
            if (tblEmpleados == null)
            {
                return NotFound();
            }

            return Ok(tblEmpleados);
        }

        // PUT: api/TblEmpleados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTblEmpleados(decimal id, TblEmpleados tblEmpleados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblEmpleados.cedula)
            {
                return BadRequest();
            }

            db.Entry(tblEmpleados).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEmpleadosExists(id))
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

        // POST: api/TblEmpleados
        [ResponseType(typeof(TblEmpleados))]
        public IHttpActionResult PostTblEmpleados(TblEmpleados tblEmpleados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TblEmpleados.Add(tblEmpleados);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TblEmpleadosExists(tblEmpleados.cedula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblEmpleados.cedula }, tblEmpleados);
        }

        // DELETE: api/TblEmpleados/5
        [ResponseType(typeof(TblEmpleados))]
        public IHttpActionResult DeleteTblEmpleados(decimal id)
        {
            TblEmpleados tblEmpleados = db.TblEmpleados.Find(id);
            if (tblEmpleados == null)
            {
                return NotFound();
            }

            db.TblEmpleados.Remove(tblEmpleados);
            db.SaveChanges();

            return Ok(tblEmpleados);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TblEmpleadosExists(decimal id)
        {
            return db.TblEmpleados.Count(e => e.cedula == id) > 0;
        }
    }
}