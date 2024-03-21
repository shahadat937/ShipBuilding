using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class FileMovementManager : IFileMovementManager
    {
        private readonly IFileMovementRepository _iFileMovementRepository;
        public FileMovementManager(IFileMovementRepository iFileMovementRepository)
        {
            _iFileMovementRepository = iFileMovementRepository;

        }
        public int CreateFileMovement(FileMovement file)
        {
            //file.EntryDate = DateTime.Today;
            //file.LastUpadate = DateTime.Today;
            return _iFileMovementRepository.Save(file);
        }
        public int InitialFileMovement(FileMovement file)
        {
            file.EntryDate = DateTime.Today;
            file.LastUpadate = DateTime.Today;
            return _iFileMovementRepository.Save(file);
        }
        public List<FileMovement> GetFileMovement()
        {
            return _iFileMovementRepository.All().ToList();
        }
        public List<FileMovement> GetFileMovementByProjectId(long? ProjectId)
        {
            return _iFileMovementRepository.Filter(x => x.ProjectId == ProjectId).ToList();
        }

        

        public FileMovement GetFileMovementById(long? Id)
        {
            return _iFileMovementRepository.FindOne(x => x.Id == Id);
        }
    }
}
