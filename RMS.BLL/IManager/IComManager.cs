using RMS.Model;
using System.Collections.Generic;
namespace RMS.BLL.IManager
{
    public interface IComManager
    {
        int Create(CommitteInfo commitee);
        List<CommitteInfo> GetCommitee();
       // CommitteInfo GetCommiteeByName(string Name);
        CommitteInfo GetCommiteeById(long Id); 
    }
}