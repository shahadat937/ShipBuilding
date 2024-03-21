using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;

namespace RMS.BLL.Manager
{
   public class FileStoreManager:IFileStoreManager
    {
             private readonly IFileStoreRepository  _fileStoreRepository;

             public FileStoreManager(IFileStoreRepository fileStoreRepository)
        {
            _fileStoreRepository = fileStoreRepository;
        }
    }
}
