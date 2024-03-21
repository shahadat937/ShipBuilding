using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using System.Collections.Generic;

namespace RMS.BLL.Manager
{
    public class ComManager:IComManager
    {
        private readonly IComRepository  _commiteeRepository;

        public ComManager(IComRepository commiteeRepository)
        {
            _commiteeRepository = commiteeRepository;
        }

        public int Create(CommitteInfo commitee)
        {
            return _commiteeRepository.Save(commitee);
        }
        public List<CommitteInfo> GetCommitee()
        {
            return _commiteeRepository.All().ToList();
        }
        //public CommitteInfo GetCommiteeByName(string Name)
        //{
        //    return _commiteeRepository.FindOne(x => x.MemberName == Name);
        //}
        public CommitteInfo GetCommiteeById(long Id)
        {
            return _commiteeRepository.FindOne(x => x.CommiteeId == Id);
        }
         
    }
}