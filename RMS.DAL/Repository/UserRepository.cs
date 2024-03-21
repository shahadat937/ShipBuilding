using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        private readonly DSBDBEntities _context;
        public UserRepository() : base()
        {
            _context = base.Context;
        }

        public int UpdateUserLoginStatus(string clientMachineName, string clientIP, long userId, bool IsValidate)
        {
            int isExecuted = 0;
            string allUserLoginSql = "update [dbo].[User] set IsLogin=0 where IsLogin = 1 and  DATEDIFF ( minute ,LastActivityDate, GETDATE())>10";
            isExecuted = _context.Database.ExecuteSqlCommand(allUserLoginSql);
            if (IsValidate)
            {
                string loginUsersql = "update [dbo].[User] set IsLogin=1, LastActivityDate=GETDATE(), HostName='" + clientMachineName + "',IPAddress='" + clientIP + "' where UserId=" + userId + "";
                isExecuted = _context.Database.ExecuteSqlCommand(loginUsersql);
   
            }

            return isExecuted;
        }

        public string CheckLogin(long userID)
        {
            string msg = "";
            string sqlCommand = "select *  from  [dbo].[User] where UserId = "+userID+" and IsLogin=1";
            string sqlQuery = string.Format(sqlCommand);
            var users = _context.Database.SqlQuery<User>(sqlQuery).ToList();
            if (users.Any())
            {
                var first = users.FirstOrDefault();
                msg = first.UserName+" user already logged in the system from another device.";
            }
            return msg;
        }
    }
}
