using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public  interface IAFDLetterManager
    {
      AFDLetter GetAFDLetterByFile(string fileId);
      List<AFDLetter> GetAfdLetterByProject(string proId);

      List<AFDLetter> GetAll();
    }
}
