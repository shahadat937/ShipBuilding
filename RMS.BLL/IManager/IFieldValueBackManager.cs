using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
   public interface IFieldValueBackManager
    {
       void Save(long? fileNo, string tenderissuedate, string value);
    }
}
