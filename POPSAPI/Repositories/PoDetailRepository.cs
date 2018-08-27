using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POPS.Models;

namespace POPSAPI.Repositories
{
    public class PoDetailRepository : IPoDetailRepository
    {
        private readonly POPSAPI.PODbEntities _context;

        public PoDetailRepository(PODbEntities context)
        {
            _context = context;
        }

        public string AddPoDetail(PoDetailModel poDetail)
        {
            string result = string.Empty;

            PODETAIL poDetailEntity = GetPoDetailEntity(poDetail);

            if (poDetailEntity != null)
            {
                _context.PODETAILs.Add(poDetailEntity);
                _context.SaveChanges();
                result = poDetailEntity.PONO;
            }
            return result;
        }

        private static PODETAIL GetPoDetailEntity(PoDetailModel poDetail)
        {
            PODETAIL poDetailEntity = new PODETAIL();
            if (poDetail != null)
            {
                poDetailEntity.PONO = poDetail.PONO;
                poDetailEntity.ITCODE= poDetail.ITCODE;
                poDetailEntity.QTY = poDetail.QTY;
            }
            return poDetailEntity;
        }

        public void DeletePoDetail(string PoNo,string iTCode)
        {
            PODETAIL poDetailEntity = _context.PODETAILs.Find(PoNo,iTCode);
            _context.PODETAILs.Remove(poDetailEntity);
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


        public List<PoDetailModel> GetAllPODetails()
        {
            List<PoDetailModel> poDetails = new List<PoDetailModel>();

            foreach (var poDetail in _context.PODETAILs)
            {
                PoDetailModel objPoDetail = GetPoDetailModel(poDetail);
                poDetails.Add(objPoDetail);
            }
            return poDetails;
        }


        private static PoDetailModel GetPoDetailModel(PODETAIL poDetailEntity)
        {
            PoDetailModel objPoDetail = new PoDetailModel();
            if (poDetailEntity != null)
            {
                objPoDetail.PONO = poDetailEntity.PONO;
                objPoDetail.ITCODE = poDetailEntity.ITCODE;
                objPoDetail.QTY = poDetailEntity.QTY;
                objPoDetail.ITDESC = poDetailEntity.ITEM.ITDESC;
                objPoDetail.PODATE = poDetailEntity.POMASTER.PODATE;
                objPoDetail.SUPLNAME = poDetailEntity.POMASTER.SUPPLIER.SUPLNAME;
                objPoDetail.SUPLNO = poDetailEntity.POMASTER.SUPPLIER.SUPLNO;
            }
            return objPoDetail;
        }


        public PoDetailModel GetPoDetailsById(string PoNo,string ITCode)
        {
            return GetPoDetailModel(_context.PODETAILs.Find(PoNo,ITCode));
        }

        public string UpdatePoDetail(PoDetailModel poDetail)
        {
            string result = string.Empty;
            PODETAIL poDetailEntity = GetPoDetailEntity(poDetail);
            if (poDetailEntity != null)
            {
                _context.Entry(poDetailEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = poDetailEntity.PONO;
            }
            return result;
        }
    }
}