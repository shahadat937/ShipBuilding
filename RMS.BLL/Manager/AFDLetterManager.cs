using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
   public class AFDLetterManager : BaseManager,IAFDLetterManager
    {
       private readonly IAFDLetterRepository _afdLetterRepository;
       private readonly IAFDLetterApproveRepository _afdLetterApproveRepository;
       public AFDLetterManager()
        {
            _afdLetterRepository = new AFDLetterRepository();
           _afdLetterApproveRepository =new AFDLetterApproveRepository();
        }

       public AFDLetter GetAFDLetterByFile(string fileId)
       {
           long identity = Convert.ToInt64(fileId);
           return _afdLetterRepository.FindOne(x => x.FileNo == identity);
       }

       public List<AFDLetter> GetAfdLetterByProject(string proId)
       {
           long ss = Convert.ToInt64(proId);
           return _afdLetterRepository.Filter(x => x.FileNo == ss).Include(x =>x.Demand).ToList();
       }

       public List<AFDLetter> GetAll()
       {
           return _afdLetterRepository.All().ToList();
       }
    }
}
