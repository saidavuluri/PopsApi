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
    public class PoMastersController : ApiController
    {
        private IPoMasterRepository _poMasterRepository;

        public PoMastersController()
        {
            _poMasterRepository = new PoMasterRepository(new PODbEntities());
        }
        public PoMastersController(IPoMasterRepository poMasterRepository)
        {
            _poMasterRepository = poMasterRepository;
        }

        // GET: api/PoMasters
        public List<PoMasterModel> GetPOMASTERs()
        {
            return _poMasterRepository.GetAllPOMasters();
        }

        // GET: api/PoMasters/5
        [ResponseType(typeof(POMASTER))]
        public IHttpActionResult GetPOMASTER(string id)
        {
            PoMasterModel poMaster = _poMasterRepository.GetPoMasterById(id);
            return Ok(poMaster);
        }

        // PUT: api/PoMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPOMASTER(string id, PoMasterModel poMaster)
        {
            string poNo = _poMasterRepository.UpdatePoMaster(poMaster);
            if (poNo == string.Empty)
            {
                throw new Exception("Falied to update");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PoMasters
        [ResponseType(typeof(POMASTER))]
        public IHttpActionResult PostPOMASTER(PoMasterModel poMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string poNo = _poMasterRepository.AddPoMaster(poMaster);

            return CreatedAtRoute("DefaultApi", new { id = poNo }, poMaster);
        }

        // DELETE: api/PoMasters/5
        [ResponseType(typeof(POMASTER))]
        public IHttpActionResult DeletePOMASTER(string id)
        {
            PoMasterModel poMaster = _poMasterRepository.GetPoMasterById(id);
            if (poMaster == null)
            {
                return NotFound();
            }
            _poMasterRepository.DeletePoMaster(id);
            return Ok(poMaster);
        }
    }
}