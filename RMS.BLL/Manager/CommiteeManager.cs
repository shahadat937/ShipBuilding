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
    public class CommiteeManager : ICommiteeManager
    {
        private readonly ICommiteeRepository  _commiteeRepository;

        public CommiteeManager(ICommiteeRepository commiteeRepository)
        {
            _commiteeRepository = commiteeRepository;
        }

        public CommitteInfo GetCommitteInfo(long commiteeId)
        {
            return _commiteeRepository.FindOne(x => x.CommiteeId == commiteeId);
        }

        public int CreateCommitte(CommitteInfo committeInfo)
        {
            return _commiteeRepository.Save(committeInfo);
        }

        public List<CommitteInfo> GetAllCommitteBuId(long? committeId)
        {
            return _commiteeRepository.Filter(x => x.CommitteName == committeId).Include(x => x.CommitteNameList).Include(x => x.Member).Include(x =>x.CommonCode).ToList();
        }

        public int DeleteCommitteInfo(long? id)
        {
          return _commiteeRepository.Delete(x => x.CommiteeId == id);
        }

        public CommitteInfo GetCommitteInfobyFileId(string fileId)
        {
            long ss = Convert.ToInt64(fileId);
            return _commiteeRepository.FindOne(x => x.FileNo == ss);
        }

        public List<CommitteInfo> GetAllCommitteByProjectId(string projectId)
        {
            long ss = Convert.ToInt64(projectId);
            return _commiteeRepository.Filter(x => x.FileNo == ss).Include(x => x.CommitteNameList).Include(x => x.Member).Include(x => x.CommonCode).ToList();

        }
    }
}
