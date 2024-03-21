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
   public class AFDLetterApproveManager : IAFDLetterApproveManager
    {
        private readonly IAFDLetterApproveRepository  _afdLetterApproveRepository;

        public AFDLetterApproveManager(IAFDLetterApproveRepository afdLetterApproveRepository)
        {
            _afdLetterApproveRepository = afdLetterApproveRepository;
        }

       public AFDLetterApprove GetAFDApprove(string fileId)
       {
           long identity = Convert.ToInt64(fileId);
           return _afdLetterApproveRepository.FindOne(x => x.AFDLetter1.FileNo == identity);
       }

       public int Create(AFDLetterApprove afdLetterApprove)
       {
           return _afdLetterApproveRepository.Save(afdLetterApprove);
       }

       public List<AFDLetterApprove> GetAll(long? proId)
       {
           return _afdLetterApproveRepository.Filter(x => x.AFDLetter1.FileNo == proId).Include(x => x.AFDLetter1).Include(x => x.AFDLetter1.Demand).ToList();
       }
    }
}
