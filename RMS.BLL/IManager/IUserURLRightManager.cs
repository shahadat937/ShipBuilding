using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IUserURLRightManager
    {
        List<UserRightTreeView> GetUserRightTreeView(string userId);

        List<UserURLRight> GetUserRighsByRole(string userRole);

        ResponseModel Save(List<UserURLRight> userURLRight);
    }
}
