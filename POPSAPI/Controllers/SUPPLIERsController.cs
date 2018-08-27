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
using POPS.Models;
using POPSAPI;
using POPSAPI.Repositories;

namespace POPSAPI.Controllers
{
    public class SUPPLIERsController : ApiController
    {
        private ISupplierRepository _supplierRepository;

        public SUPPLIERsController()
        {
            _supplierRepository = new SupplierRepository(new PODbEntities());
        }
        public SUPPLIERsController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        // GET: api/SUPPLIERs
        public List<SupplierModel> GetSUPPLIERs()
        {
            return _supplierRepository.GetAllSuppliers();
        }

        // GET: api/SUPPLIERs/5
        [ResponseType(typeof(SUPPLIER))]
        public IHttpActionResult GetSUPPLIER(string id)
        {
            SupplierModel supplier = _supplierRepository.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // PUT: api/SUPPLIERs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSUPPLIER(string id, [FromBody]SupplierModel supplier)
        {
           
           string suplNo = _supplierRepository.UpdateSupplier(supplier);
            if(suplNo== string.Empty)
            {
                throw new Exception("Falied to update");
            }

            return Content(HttpStatusCode.Accepted, supplier);
        }

        // POST: api/SUPPLIERs
        [ResponseType(typeof(SUPPLIER))]
        public IHttpActionResult PostSUPPLIER(SupplierModel supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string suplNo = _supplierRepository.AddSupplier(supplier);
            
            return CreatedAtRoute("DefaultApi", new { id = suplNo }, supplier);
        }

        // DELETE: api/SUPPLIERs/5
        [ResponseType(typeof(SUPPLIER))]
        public IHttpActionResult DeleteSUPPLIER(string id)
        {
            SupplierModel supplier = _supplierRepository.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            _supplierRepository.DeleteSupplier(id);
            return Ok();
        }
    }
}