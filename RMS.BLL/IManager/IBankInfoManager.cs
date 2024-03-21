using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IBankInfoManager
    {
        BankInfo FindOne(long branchInfoIdentity);
    }
}
