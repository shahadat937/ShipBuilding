using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface ICommitteNameListManager
    {
       CommitteNameList GetCommitteName(string committeName);
       int CreateCommitteName(CommitteNameList committeNameList);
       List<CommitteNameList> GetAll();
    }
}
