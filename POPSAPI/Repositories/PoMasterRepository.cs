using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POPS.Models;

namespace POPSAPI.Repositories
{
    public class PoMasterRepository : IPoMasterRepository
    {
        private readonly POPSAPI.PODbEntities _context;

        public PoMasterRepository(PODbEntities context)
        {
            _context = context;
        }

        public string AddPoMaster(PoMasterModel poMaster)
        {
            string result = string.Empty;

            POMASTER poMasterEntity = GetPoMasterEntity(poMaster);

            if (poMasterEntity != null)
            {
                _context.POMASTERs.Add(poMasterEntity);
                _context.SaveChanges();
                result = poMasterEntity.PONO;
            }
            return result;
        }


        private static POMASTER GetPoMasterEntity(PoMasterModel poMaster)
        {
            POMASTER poMasterEntity = new POMASTER();
            if (poMaster != null)
            {
                poMasterEntity.PONO = poMaster.PONO;
                poMasterEntity.PODATE = poMaster.PODATE;
                poMasterEntity.SUPLNO = poMaster.SUPLNO;
            }
            return poMasterEntity;
        }

        public void DeletePoMaster(string PoNo)
        {
            POMASTER poMasterEntity = _context.POMASTERs.Find(PoNo);
            _context.POMASTERs.Remove(poMasterEntity);
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


        public List<PoMasterModel> GetAllPOMasters()
        {
            List<PoMasterModel> poMasters = new List<PoMasterModel>();

            foreach (var poMaster in _context.POMASTERs)
            {
                PoMasterModel objPoMaster = GetPoMasterModel(poMaster);
                poMasters.Add(objPoMaster);
            }
            return poMasters;
        }

        private static PoMasterModel GetPoMasterModel(POMASTER poMasterEntity)
        {
            PoMasterModel objPoMaster = new PoMasterModel();
            if (poMasterEntity != null)
            {
                objPoMaster.PODATE = poMasterEntity.PODATE;
                objPoMaster.PONO = poMasterEntity.PONO;
                objPoMaster.SUPLNO = poMasterEntity.SUPLNO;
                objPoMaster.SUPLNAME = poMasterEntity.SUPPLIER?.SUPLNAME;
            }
            return objPoMaster;
        }


        public PoMasterModel GetPoMasterById(string PoNo)
        {
            return GetPoMasterModel(_context.POMASTERs.Find(PoNo));
        }

        public string UpdatePoMaster(PoMasterModel poMaster)
        {
            string result = string.Empty;
            POMASTER poMasterEntity = GetPoMasterEntity(poMaster);
            if (poMasterEntity != null)
            {
                _context.Entry(poMasterEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = poMasterEntity.PONO;
            }
            return result;
        }
    }
}