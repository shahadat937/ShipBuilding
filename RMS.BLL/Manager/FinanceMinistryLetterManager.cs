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
   public class FinanceMinistryLetterManager : BaseManager,IFinanceMinistryLetterManager
   {
       private readonly IFinanceMinistryLetterRepository _financeMinistryLetter;

       public FinanceMinistryLetterManager(IFinanceMinistryLetterRepository financeMinistryLetter)
       {
           _financeMinistryLetter = financeMinistryLetter;
       }

       public FinanceMinistryLetter GetFinanceMinistryLetterByFile(string fileId)
       {
           long identity = Convert.ToInt64(fileId);
           return _financeMinistryLetter.FindOne(x => x.FileNo == identity);
       }

       public List<FinanceMinistryLetter> GetFinanceMinistryByProject(string proId)
       {
           long ss = Convert.ToInt64(proId);
           return _financeMinistryLetter.Filter(x => x.FileNo == ss).Include(x =>x.Demand).ToList();
       }
   }
}
