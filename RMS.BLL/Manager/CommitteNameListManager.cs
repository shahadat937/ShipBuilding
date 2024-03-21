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
   public class CommitteNameListManager : ICommitteNameListManager
    {
         private readonly ICommitteNameListRepository  _committeNameListRepository;
        public CommitteNameListManager(ICommitteNameListRepository committeNameListRepository)
        {
            _committeNameListRepository = committeNameListRepository;
        }

       public CommitteNameList GetCommitteName(string committeName)
       {
           return _committeNameListRepository.FindOne(x =>x.CommitteName.ToLower() == committeName.ToLower());
       }

       public int CreateCommitteName(CommitteNameList committeNameList)
       {
           return _committeNameListRepository.Save(committeNameList);
       }

       public List<CommitteNameList> GetAll()
       {
           return _committeNameListRepository.All().ToList();
       }
    }
}
