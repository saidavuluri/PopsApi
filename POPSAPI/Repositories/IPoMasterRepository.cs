using POPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSAPI.Repositories
{
    public interface IPoMasterRepository : IDisposable
    {
        List<PoMasterModel> GetAllPOMasters();
        PoMasterModel GetPoMasterById(string PoNo);
        string AddPoMaster(PoMasterModel poMaster);
        string UpdatePoMaster(PoMasterModel poMaster);
        void DeletePoMaster(string PoNo);
    }
}
