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

        // GET: api/PoDetails/5/AA
        [HttpGet, Route("api/PODetails/{id}/{iTCode}")]
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult GetPODETAIL(string id,string iTCode)
        {
            PoDetailModel pODETAIL = _poDetailsRepository.GetPoDetailsById(id, iTCode);
            if (pODETAIL == null)
            {
                return NotFound();
            }

            return Ok(pODETAIL);
        }

        // PUT: api/PoDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPODETAIL(string id, PoDetailModel poDetail)
        {
            string poNo = _poDetailsRepository.UpdatePoDetail(pODETAIL);
            if (poNo == string.Empty)
            {
                throw new Exception("Falied to update");
            }

            return Content(HttpStatusCode.Accepted, poDetail);
        }

        // POST: api/PoDetails
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult PostPODETAIL(PoDetailModel poDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string poNo = _poDetailsRepository.AddPoDetail(poDetail);


            return CreatedAtRoute("DefaultApi", new { id = poDetail.PONO }, poDetail);
        }

        // DELETE: api/PoDetails/5
        [HttpDelete, Route("api/PODetails/{id}/{iTCode}")]
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult DeletePODETAIL(string id,string iTCode)
        {
            PoDetailModel poDetail = _poDetailsRepository.GetPoDetailsById(id, iTCode);
            if (poDetail == null)
            {
                return NotFound();
            }
            _poDetailsRepository.DeletePoDetail(id,iTCode);
            return Ok();
        }
    }
}