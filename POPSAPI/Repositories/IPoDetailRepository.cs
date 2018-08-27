using POPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSAPI.Repositories
{
    public interface IPoDetailRepository : IDisposable
    {
        List<PoDetailModel> GetAllPODetails();
        PoDetailModel GetPoDetailsById(string PoNo,string iTCode);
        string AddPoDetail(PoDetailModel poDetail);
        string UpdatePoDetail(PoDetailModel poDetail);
        void DeletePoDetail(string PoNo,string iTCode);
    }
}
