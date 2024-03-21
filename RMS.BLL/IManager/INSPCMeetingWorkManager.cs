using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
   public interface INSPCMeetingWorkManager
    {
       List<NSPCMeetingWork> GetAllNSPCMeetingWork();
       List<NSPCMeetingWork>GetNSPCMeetingWorksBySearchKey(string searchKey);
       string SaveNSPCMeetingWork(NSPCMeetingWork nspcMeetingWork);
       NSPCMeetingWork GetNSPCMeetingWorkById(long nspcMeetingWorkId);
       string EditNSPCMeetingWork(NSPCMeetingWork nspcMeetingWork);
       int DeleteNSPCMeetingWork(NSPCMeetingWork nspcMeetingWork);
    }
}
