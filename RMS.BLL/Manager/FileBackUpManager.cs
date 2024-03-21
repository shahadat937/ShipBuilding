using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class FileBackUpManager : IFileBackUpManager
    {
      private readonly IFileBackUpRepository  _fileBackUpRepository;

       public FileBackUpManager(IFileBackUpRepository fileBackUpRepository)
        {
            _fileBackUpRepository = fileBackUpRepository;
        }

       public void Save(string formId, string approveUrl)
       {
           FileBackUp model = new FileBackUp();
           model.FormId =Convert.ToInt64(formId);
           model.FileUrl = approveUrl;
           model.CreateDate = DateTime.Now.Date;
           model.CreateId = PortalContext.CurrentUser.UserName;
           _fileBackUpRepository.Save(model);
       }
    }
}
