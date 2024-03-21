//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RMS.BLL.IManager;
//using RMS.DAL.IRepository;
//using RMS.Model;

//namespace RMS.BLL.Manager
//{
//    public class DepartmentManager : IDepartmentManager
//    {
//        private readonly IDepartmentRepositoty _departmentRepositoty;

//        public DepartmentManager(IDepartmentRepositoty departmentRepositoty)
//        {
//            _departmentRepositoty = departmentRepositoty;
//        }

//        public int Create(Department model)
//        {
//            int result = _departmentRepositoty.Save(model);
//            return result;
//        }
//        public int Delete(Department model)
//        {
//            int result = _departmentRepositoty.Delete(model);
//            return result;
//        }
//        public List<Department> GetAllDepartments()
//        {
//            return _departmentRepositoty.All().ToList();
//        }
//        public List<Department> GetDepartmentsByName( string Name)
//        {
//            return _departmentRepositoty.Filter(x=>x.Name==Name).ToList();
//        }
//        public Department GetDepartmentsById(long? Id)
//        {
//            return _departmentRepositoty.FindOne(x => x.DepartmentId == Id);
//        }

//        public long GetDepId(string last)
//        {
//            return _departmentRepositoty.FindOne(x => x.Name.ToLower() == last.ToLower()).DepartmentId;
//        }

//        public string GetDeptNameByDeptId(long id)
//        {
//            return _departmentRepositoty.FindOne(x => x.DepartmentId == id).Name;
//        }
//    }



//}


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
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepositoty _departmentRepositoty;

        public DepartmentManager(IDepartmentRepositoty departmentRepositoty)
        {
            _departmentRepositoty = departmentRepositoty;
        }

        public int Create(Department model)
        {
            int result = _departmentRepositoty.Save(model);
            return result;
        }
        public int Delete(Department model)
        {
            int result = _departmentRepositoty.Delete(model);
            return result;
        }
        public List<Department> GetAllDepartments()
        {
            return _departmentRepositoty.All().ToList();
        }
        public List<Department> GetDepartmentsByName(string Name)
        {
            return _departmentRepositoty.Filter(x => x.Name == Name).ToList();
        }
        public Department GetDepartmentsById(long? Id)
        {
            return _departmentRepositoty.FindOne(x => x.DepartmentId == Id);
        }

        public long GetDepId(string last)
        {
            return _departmentRepositoty.FindOne(x => x.Name.ToLower() == last.ToLower()).DepartmentId;
        }

        public string GetDeptNameByDeptId(long id)
        {
            return _departmentRepositoty.FindOne(x => x.DepartmentId == id).Name;
        }
        public void DeleteDepartmentById(Department department)
        {
            _departmentRepositoty.Delete(x => x.DepartmentId == department.DepartmentId);
        }
    }



}

