using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public interface IMemberManager
    {
      int Create(Member member);
      List<Member> GetMember();
      Member GetMemberByName(string MemberName);
      
      Member GetMemberById(long? Id);
      List<Member> GetAll();

      List<Member> GetAllMembersBySearchKeyword(string searchKey);
    }
}
