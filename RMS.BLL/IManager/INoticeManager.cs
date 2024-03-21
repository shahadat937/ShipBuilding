using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public  interface INoticeManager
    {
        List<Notice> GetNotices();

        Notice FindOne(int noticeInfoIdentity);

        ResponseModel Save(Notice notice);

        ResponseModel Delete(int p);

        Notice FindOneLast();


        int UpdateLogInStatus(string clientMachineName, string clientIp, long userId, bool isValidate);

        int UpdateLogOffStatus(long userId);
    }
}
