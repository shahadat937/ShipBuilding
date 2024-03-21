using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IFinanceMinistryLetterManager
    {
       FinanceMinistryLetter GetFinanceMinistryLetterByFile(string fileId);
       List<FinanceMinistryLetter> GetFinanceMinistryByProject(string proId);
    }
}
