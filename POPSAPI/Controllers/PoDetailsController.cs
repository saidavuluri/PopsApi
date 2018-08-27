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
    public class PoDetailsController : ApiController
    {
        private IPoDetailRepository _poDetailsRepository;

        public PoDetailsController()
        {
            _poDetailsRepository = new PoDetailRepository(new PODbEntities());
        }
        public PoDetailsController(IPoDetailRepository poDetailsRepository)
        {
            _poDetailsRepository = poDetailsRepository;
        }

        // GET: api/PoDetails
        public List<PoDetailModel> GetPODETAILs()
        {
            return _poDetailsRepository.GetAllPODetails();
        }

        // GET: api/PoDetails/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult GetPODETAIL(string poNo)
        {
            PoDetailModel pODETAIL = _poDetailsRepository.GetPoDetailsById(poNo);
            if (pODETAIL == null)
            {
                return NotFound();
            }

            return Ok(pODETAIL);
        }

        // PUT: api/PoDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPODETAIL(string id, PoDetailModel pODETAIL)
        {
            string poNo = _poDetailsRepository.UpdatePoDetail(pODETAIL);
            if (poNo == string.Empty)
            {
                throw new Exception("Falied to update");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PoDetails
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult PostPODETAIL(PoDetailModel pODETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string poNo = _poDetailsRepository.AddPoDetail(pODETAIL);


            return CreatedAtRoute("DefaultApi", new { id = pODETAIL.PONO }, pODETAIL);
        }

        // DELETE: api/PoDetails/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult DeletePODETAIL(string id)
        {
            PoDetailModel poDetail = _poDetailsRepository.GetPoDetailsById(id);
            if (poDetail == null)
            {
                return NotFound();
            }
            _poDetailsRepository.DeletePoDetail(id);
            return Ok(poDetail);
        }
    }
}