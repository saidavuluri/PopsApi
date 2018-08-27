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
    public class ItemsController : ApiController
    {

        private IItemRepository _itemRepository;

        public ItemsController()
        {
            _itemRepository = new ItemRepository(new PODbEntities());
        }
        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }


        // GET: api/Items
        public List<ITEMModel> GetITEMs()
        {
            return _itemRepository.GetAllItems();
        }

        // GET: api/Items/5
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult GetITEM(string id)
        {
            ITEMModel iTEM = _itemRepository.GetItemByItemCode(id);
            if (iTEM == null)
            {
                return NotFound();
            }

            return Ok(iTEM);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutITEM(string id, ITEMModel item)
        {
            string itCode = _itemRepository.UpdateItem(item);
            if (itCode == string.Empty)
            {
                throw new Exception("Falied to update");
            }

            return Content(HttpStatusCode.Accepted, item);
        }

        // POST: api/Items
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult PostITEM(ITEMModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string poNo = _itemRepository.AddItem(item);
            return CreatedAtRoute("DefaultApi", new { id = item.ITCODE }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult DeleteITEM(string id)
        {
            ITEMModel item = _itemRepository.GetItemByItemCode(id);
            if (item == null)
            {
                return NotFound();
            }
            _itemRepository.DeleteItem(item.ITCODE);
            return Ok();
        }
    }
}