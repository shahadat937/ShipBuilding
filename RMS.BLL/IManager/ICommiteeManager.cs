using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface ICommiteeManager
    {
        CommitteInfo GetCommitteInfo(long commiteeId);
        int CreateCommitte(CommitteInfo committeInfo);
        List<CommitteInfo> GetAllCommitteBuId(long? committeId);
        int DeleteCommitteInfo(long? id);
        CommitteInfo GetCommitteInfobyFileId(string fileId);
        List<CommitteInfo> GetAllCommitteByProjectId(string projectId);
    }
}
