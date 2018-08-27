using POPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPSAPI.Repositories
{
    public interface IItemRepository : IDisposable
    {
        List<ITEMModel> GetAllItems();
        ITEMModel GetItemByItemCode(string ItemCode);
        string AddItem(ITEMModel itemModel);
        string UpdateItem(ITEMModel itemModel);
        void DeleteItem(string PoNo);
    }
}
