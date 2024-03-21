using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class MemberManager : IMemberManager
    {
        private readonly IMemberRepository _memberRepository;

        public MemberManager(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public int Create(Member member)
        {
            return _memberRepository.Save(member);
        }
        public List<Member> GetMember()
        {
            return _memberRepository.All().ToList();
        }
        public Member GetMemberByName(string MemberName)
        {
            return _memberRepository.FindOne(x => x.MemberName == MemberName);
        }
        public Member GetMemberById(long? Id)
        {
            return _memberRepository.FindOne(x => x.MemberId == Id);
        }

        public List<Member> GetAll()
        {
            return _memberRepository.All().ToList();
        }


        public List<Member> GetAllMembersBySearchKeyword(string searchKeyword)
        {
            return _memberRepository.Filter(x => x.OfficeId.Contains(searchKeyword) || x.MemberName.ToLower().Contains(searchKeyword.ToLower())
                || x.Email.ToLower().Contains(searchKeyword.ToLower()) || x.PhoneNo.Contains(searchKeyword)).ToList();
        }
    }
}
