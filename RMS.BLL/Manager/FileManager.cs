using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class FileManager : IFileManager
    {
        private readonly IFileRepository _iFileRepository;
        public FileManager(IFileRepository iFileRepository)
        {
            _iFileRepository = iFileRepository;

        }
        public int CreateFile(File file)
        {
            file.EntyDate = DateTime.Today;
            file.LastUpdate = DateTime.Today;
            return _iFileRepository.Save(file);
        }

        public List<File> GetFilesByProjectId(long id)
        {
            return _iFileRepository.Filter(x => x.ProjectId == id).ToList();
        }
        public List<File> GetFiles()
        {
            return _iFileRepository.All().ToList();
        }
        public List<File> GetFilesByFileName(string FileName)
        {
            return _iFileRepository.Filter(x=>x.FileName==FileName).ToList();
        }
        public List<File> GetFilesByFileCode(string FileCode)
        {
            return _iFileRepository.Filter(x => x.FileCode == FileCode).ToList();
        }
        public File GetFilesByFileId(long Id)
        {
            return _iFileRepository.FindOne(x=> x.FileId==Id);
        }
    }
}
