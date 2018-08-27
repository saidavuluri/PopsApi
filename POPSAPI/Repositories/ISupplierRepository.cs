using POPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSAPI.Repositories
{
   public interface ISupplierRepository : IDisposable
    {
        List<SupplierModel> GetAllSuppliers();
        SupplierModel GetSupplierById(string SuplNo);
        string AddSupplier(SupplierModel supplierEntity);
        string UpdateSupplier(SupplierModel supplierEntity);
        void DeleteSupplier(string SuplNo);
    }
}
