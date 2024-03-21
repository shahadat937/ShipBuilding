using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IFileMovementManager
    {
        int CreateFileMovement(FileMovement fileMovement);
        int InitialFileMovement(FileMovement fileMovement);
        List<FileMovement> GetFileMovement();
        List<FileMovement> GetFileMovementByProjectId(long? ProjectId);
        FileMovement GetFileMovementById(long? Id);
    }
}
