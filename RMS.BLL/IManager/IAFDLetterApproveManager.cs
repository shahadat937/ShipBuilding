using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IAFDLetterApproveManager
    {
       AFDLetterApprove GetAFDApprove(string fileId);
       int Create(AFDLetterApprove afdLetterApprove);
       List<AFDLetterApprove> GetAll(long? proId);
    }
}
