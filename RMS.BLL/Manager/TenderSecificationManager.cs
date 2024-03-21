using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using File = System.IO.File;

namespace RMS.BLL.Manager
{
   public class TenderSecificationManager : ITenderSecificationManager
    {
      private readonly ITenderSecificationRepository _tenderSecificationRepository;
       private readonly ITenderSpecificationChildRepository _tenderSpecificationChildRepository;
      public TenderSecificationManager()
        {
            _tenderSecificationRepository = new TenderSecificationRepository();
          _tenderSpecificationChildRepository =new TenderSpecificationChildRepository();
        }

       public List<TenderSpecification> GetAll()
       {
           return _tenderSecificationRepository.All().ToList();
       }

       public TenderSpecification GetTenderSpecification(long? tenderId)
       {
           return _tenderSecificationRepository.FindOne(x => x.TenderIdentity == tenderId);
       }

       public int Create(TenderSpecification tenderSecification)
       {
           return _tenderSecificationRepository.Save(tenderSecification);
       }

       public List<TenderSpecification> GetSearchData(string searchKey)
       {
           return
               _tenderSecificationRepository.Filter(
                   x =>
                       x.Title.ToLower().Contains(searchKey)).ToList();
       }

       public TenderSpecification GettenderSecificationByFile(string fileId)
       {
           var fileid = Convert.ToInt64(fileId);
           return _tenderSecificationRepository.FindOne(x => x.FileNo == fileid);
       }

       public TenderSpecification GetTenderType(long? fileNo)
       {
           return _tenderSecificationRepository.FindOne(x => x.FileNo == fileNo);
       }

       public List<TenderSpecification> GetAllByFileId(string fileId)
       {
           var fileid = Convert.ToInt64(fileId);
           return _tenderSecificationRepository.Filter(x => x.FileNo == fileid).ToList();
       }

       public List<TenderSpecificationChild> GetAllTenderChildByDemandId(string fileId)
       {
           long? pId = fileId != null ? Convert.ToInt64(fileId) : 0;
           return _tenderSpecificationChildRepository.Filter(x => x.ProjectId == pId).ToList();
       }

       public void SaveTederChild(List<TenderSpecificationChild> tenderChilds, long? fileNo)
       {
           foreach (var item in tenderChilds)
           {
               TenderSpecificationChild model = new TenderSpecificationChild();
               TenderSpecificationChild tender = _tenderSpecificationChildRepository.FindOne(x => x.Id == item.Id);
               if (tender != null)
               {
                   tender.FieldName = item.FieldName;
                   tender.IsChecked = item.IsChecked;
                   tender.FieldValue = item.FieldValue;
                   tender.LastUpdateDate = item.CreateDate;
                   tender.LastUpdateId = PortalContext.CurrentUser.UserName;
                   model = tender;
               }
               else
               {
                   model.ProjectId = (long)fileNo;
                   model.Id = item.Id;
                   model.FieldName = item.FieldName;
                   model.IsChecked = item.IsChecked;
                   model.FieldValue = item.FieldValue;
                   model.CreateDate = item.CreateDate;
                   model.CreateId = PortalContext.CurrentUser.UserName;
               }
               _tenderSpecificationChildRepository.Save(model);
           }
       }

       public void DeleteTenderChild(int? id)
       {
           _tenderSpecificationChildRepository.Delete(x => x.Id == id);
       }
    }
}
