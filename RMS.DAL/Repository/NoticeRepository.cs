using System;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class NoticeRepository : Repository<Notice>,INoticeRepository
     {
        private readonly DSBDBEntities _context;
        public NoticeRepository() : base()
        {
            _context = base.Context;
        }

        public int UpdateUserLoginStatus(string clientMachineName, string clientIP, long userId,bool isValidate)
        {
            int isExecuted = 0;
            //string loginUsersql = "update [dbo].[User] set IsLogin=1, LastActivityDate=GETDATE(), HostName='" + clientMachineName + "',IPAddress='" + clientIP + "' where UserId=" + userId + "";
            //isExecuted = _context.Database.ExecuteSqlCommand(loginUsersql);
            string allUserLoginSql = "update [dbo].[User] set IsLogin=0 where IsLogin = 1 and  DATEDIFF ( minute ,LastActivityDate, GETDATE())>10";
            isExecuted = _context.Database.ExecuteSqlCommand(allUserLoginSql);
            return isExecuted;
        }

        public int UpdateLogOff(long userId)
        {
            int isUpdate = 0;
            string updateSql = "update [dbo].[User] set IsLogin=0,LastActivityDate=GETDATE() where UserId = " + userId + "";
            isUpdate = _context.Database.ExecuteSqlCommand(updateSql);
            return isUpdate;
        }
     }
}
