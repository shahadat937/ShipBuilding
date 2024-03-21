//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RMS.Model;

//namespace RMS.BLL.IManager
//{
//    public interface IDepartmentManager
//    {
//        int Create(Department model);
//        int Delete(Department model);
//        List<Department> GetAllDepartments();
//        List<Department> GetDepartmentsByName(string Name);
//        Department GetDepartmentsById(long? Id);
//        long GetDepId(string last);

//        string GetDeptNameByDeptId(long id);
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IDepartmentManager
    {
        int Create(Department model);
        int Delete(Department model);
        List<Department> GetAllDepartments();
        List<Department> GetDepartmentsByName(string Name);
        Department GetDepartmentsById(long? Id);
        long GetDepId(string last);

        string GetDeptNameByDeptId(long id);

        void DeleteDepartmentById(Department department);
    }
}

