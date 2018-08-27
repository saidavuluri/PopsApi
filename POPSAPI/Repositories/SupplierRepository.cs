using POPS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace POPSAPI.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {

        private readonly POPSAPI.PODbEntities _context;

        public SupplierRepository(PODbEntities context)
        {
            _context = context;
        }

        public string AddSupplier(SupplierModel supplier)
        {
            string result = string.Empty;

            SUPPLIER supplierEntity = GetSupplierEntity(supplier);

            if (supplierEntity != null)
            {
                _context.SUPPLIERs.Add(supplierEntity);
                _context.SaveChanges();
                result = supplierEntity.SUPLNO;
            }
            return result;
        }

        private static SUPPLIER GetSupplierEntity(SupplierModel supplier)
        {
            SUPPLIER supplierEntity = new SUPPLIER();
            if (supplier != null)
            {
                supplierEntity.SUPLADDR = supplier.SUPLADDR;
                supplierEntity.SUPLNAME = supplier.SUPLNAME;
                supplierEntity.SUPLNO = supplier.SUPLNO;
            }
            return supplierEntity;
        }

        public void DeleteSupplier(string SuplNo)
        {
            SUPPLIER supplierEntity = _context.SUPPLIERs.Find(SuplNo);
            _context.SUPPLIERs.Remove(supplierEntity);
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

        public List<SupplierModel> GetAllSuppliers()
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();

            foreach (var supplier in _context.SUPPLIERs)
            {
                SupplierModel objSupplier = GetSupplierModel(supplier);
                suppliers.Add(objSupplier);
            }
            return suppliers;
        }

        private static SupplierModel GetSupplierModel(SUPPLIER supplierEntity)
        {
            SupplierModel objSupplier = new SupplierModel();
            if (supplierEntity != null)
            {
                objSupplier.SUPLADDR = supplierEntity.SUPLADDR;
                objSupplier.SUPLNAME = supplierEntity.SUPLNAME;
                objSupplier.SUPLNO = supplierEntity.SUPLNO;
            }
            return objSupplier;
        }

        public SupplierModel GetSupplierById(string suplNo)
        {
            return GetSupplierModel(_context.SUPPLIERs.Find(suplNo));
        }

        public string UpdateSupplier(SupplierModel supplier)
        {
            string result = string.Empty;
            SUPPLIER supplierEntity = GetSupplierEntity(supplier);
            if (supplierEntity != null)
            {
                _context.Entry(supplierEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = supplierEntity.SUPLNO;
            }
            return result;
        }
    }
}