﻿using System;
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
        public IHttpActionResult GetITEM(string itemCode)
        {
            ITEMModel iTEM = _itemRepository.GetItemByItemCode(itemCode);
            if (iTEM == null)
            {
                return NotFound();
            }

            return Ok(iTEM);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutITEM(string itemCode, ITEMModel iTEM)
        {
            string itCode = _itemRepository.UpdateItem(iTEM);
            if (itCode == string.Empty)
            {
                throw new Exception("Falied to update");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Items
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult PostITEM(ITEMModel iTEM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string poNo = _itemRepository.AddItem(iTEM);
            return CreatedAtRoute("DefaultApi", new { id = iTEM.ITCODE }, iTEM);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult DeleteITEM(string itemCode)
        {
            ITEMModel item = _itemRepository.GetItemByItemCode(itemCode);
            if (item == null)
            {
                return NotFound();
            }
            _itemRepository.DeleteItem(item.ITCODE);
            return Ok(item);
        }
    }
}