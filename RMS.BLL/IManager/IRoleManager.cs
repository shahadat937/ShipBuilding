using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IRoleManager
    {
        List<Role> GetAll();
        Role FindOne(int roleId);
    }
}
