using RMS.Model;

namespace RMS.DAL.IRepository
{
    public interface INoticeRepository : IRepository<Notice>
    {

        int UpdateUserLoginStatus(string clientMachineName, string clientIP, long userId, bool isValidate);

        int UpdateLogOff(long userId);
    }
}
