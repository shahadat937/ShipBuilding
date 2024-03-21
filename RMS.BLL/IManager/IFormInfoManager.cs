using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IFormInfoManager
    {
       FormInfo FindbyId(long id);
       List<FormInfo> GetAll();

       List<FormInfo> GetSpecificFormInfoById(string id, string fileName);
    }
}
