using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface ICommitteeRaiseLetterManager
    {
        string SaveCommitteeRaiseLetter(CommitteeRaiseLetter committeeRaiseLetter, string dirFullPath, string flowSerial, string formId);

        List<CommitteeRaiseLetter> GetCommitteeRaiseLetters();
        List<CommitteeRaiseLetter> GetCommitteeRaiseLettersBySearchKey(string searchKey);

        CommitteeRaiseLetter FindCommitteeRaiseLetterById(long CommitteeRaiseLetterId);

        int DeleteCommitteeRaiseLetter(CommitteeRaiseLetter committeeRaiseLetter);

        string EditCommitteeRaiseLetter(CommitteeRaiseLetter committeeRaiseLetter);
        CommitteeRaiseLetter GetCommitteeRaiseLetterByFile(string fileId);
        List<CommitteeRaiseLetter> GetCommitteeRaiseLettersbyProId(string proId);
    }
}
