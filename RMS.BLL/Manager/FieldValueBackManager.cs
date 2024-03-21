using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
   public class FieldValueBackManager:BaseManager,IFieldValueBackManager
    {
          private readonly IFieldValueBackRepository _fieldValueBackRepository;
    
       public FieldValueBackManager()
        {

            _fieldValueBackRepository = new FieldValueBackRepository();
        }

       public void Save(long? fileNo, string tenderissuedate, string value)
       {
           FieldValueBack mm = new FieldValueBack();
           mm.ProjectId = (int?) fileNo;
           mm.FieldName = tenderissuedate;
           mm.Value = value;
           _fieldValueBackRepository.Save(mm);
       }
    }
}
