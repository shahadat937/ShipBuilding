using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class FileStoreRepository : Repository<FileStore>, IFileStoreRepository
  {
        public FileStoreRepository()
           : base()
        {
        }
    }
}
