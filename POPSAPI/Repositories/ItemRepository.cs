using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POPS.Models;

namespace POPSAPI.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly POPSAPI.PODbEntities _context;

        public ItemRepository(PODbEntities context)
        {
            _context = context;
        }


        public string AddItem(ITEMModel itemModel)
        {
            string result = string.Empty;

            ITEM itemEntity = GetItemEntity(itemModel);

            if (itemEntity != null)
            {
                _context.ITEMs.Add(itemEntity);
                _context.SaveChanges();
                result = itemEntity.ITCODE;
            }
            return result;
        }

        private static ITEM GetItemEntity(ITEMModel itemModel)
        {
            ITEM itemEntity = new ITEM();
            if (itemModel != null)
            {
                itemEntity.ITCODE = itemModel.ITCODE;
                itemEntity.ITDESC = itemModel.ITDESC;
                itemEntity.ITRATE = itemModel.ITRATE;
            }
            return itemEntity;
        }


        public void DeleteItem(string iTCode)
        {
            ITEM itemEntity = _context.ITEMs.Find(iTCode);
            _context.ITEMs.Remove(itemEntity);
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public List<ITEMModel> GetAllItems()
        {
            List<ITEMModel> items = new List<ITEMModel>();

            foreach (var item in _context.ITEMs)
            {
                ITEMModel objItem = GetItemModel(item);
                items.Add(objItem);
            }
            return items;
        }


        private static ITEMModel GetItemModel(ITEM itemEntity)
        {
            ITEMModel objItem = new ITEMModel();
            if (itemEntity != null)
            {
                objItem.ITCODE = itemEntity.ITCODE;
                objItem.ITDESC = itemEntity.ITDESC;
                objItem.ITRATE = itemEntity.ITRATE;
            }
            return objItem;
        }

        public ITEMModel GetItemByItemCode(string itemCode)
        {
            return GetItemModel(_context.ITEMs.Find(itemCode));
        }

        public string UpdateItem(ITEMModel itemModel)
        {
            string result = string.Empty;
            ITEM itemEntity = GetItemEntity(itemModel);
            if (itemEntity != null)
            {
                _context.Entry(itemEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = itemEntity.ITCODE;
            }
            return result;
        }
    }
}