using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface ITenderSecificationManager
    {
       List<TenderSpecification> GetAll();
       TenderSpecification GetTenderSpecification(long? tenderId);
       int Create(TenderSpecification tenderSecification);
       List<TenderSpecification> GetSearchData(string searchKey);
       TenderSpecification GettenderSecificationByFile(string fileId);
       TenderSpecification GetTenderType(long? fileNo);
       List<TenderSpecification> GetAllByFileId(string fileId);

       List<TenderSpecificationChild> GetAllTenderChildByDemandId(string fileId);
       void SaveTederChild(List<TenderSpecificationChild> tenderChilds, long? fileNo);
       void DeleteTenderChild(int? id);
    }
}
