using RMS.DAL.IRepository;
using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RMS.DAL.Repository
{
    public class FileUploadFormRepository : Repository<FileUploadForm>, IFileUploadFormRepository
    {
        public FileUploadFormRepository()
           : base()
        {
        }
    }
}
