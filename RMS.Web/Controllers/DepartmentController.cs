using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Web.Models.ViewModel;
using RMS.Utility;

namespace RMS.Web.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentManager _iDepartmentManager;
        public DepartmentController(IDepartmentManager iDepartmentManager)
        {
            _iDepartmentManager = iDepartmentManager;
        }
        public ActionResult Index( DepartmentViewModel model)
        {
            model.Departments = _iDepartmentManager.GetAllDepartments();
            return View(model);
        }
        public ActionResult SearchByKey(DepartmentViewModel model)
        {
          
            if (model.SearchKey == null)
            {
                model.FailedMessage = "Please enter a keyword to search";
                model.Departments = _iDepartmentManager.GetAllDepartments();
            }
            else
            {
                string searchKey = model.SearchKey.ToLower();
                searchKey = searchKey.ToLower();
                model.Departments = _iDepartmentManager.GetAllDepartments().Where(x => x.Name.ToLower().Contains(searchKey)).ToList();
                if (!model.Departments.Any())
                {
                    //model.SuccessMessage = 0;
                    model.FailedMessage = "Data is not found.";
                }
            }
            return View("Index", model);
        }


        //public ActionResult SearchByKey(DepartmentViewModel model)
        //{
        //    string searchKey = model.Department.Name;
        //    if (searchKey == null)
        //    {
        //        model.Departments = _iDepartmentManager.GetAllDepartments();
        //    }
        //    else
        //    {
        //        searchKey = searchKey.ToLower();
        //        model.Departments = _iDepartmentManager.GetAllDepartments().Where(x => x.Name.ToLower().Contains(searchKey)).ToList();
        //        if (!model.Departments.Any())
        //        {
        //            //model.SuccessMessage = 0;
        //            model.FailedMessage = "Data is not found.";
        //        }
        //    }
        //    return View("Index", model);
        //}
        [HttpGet]
        public ActionResult Create(string DepartmentId, DepartmentViewModel model)
        {
            if (DepartmentId != null)
            {
                model.Department = _iDepartmentManager.GetDepartmentsById(Convert.ToInt64(DepartmentId));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    RMS.Model.Department Dep = new RMS.Model.Department();
                    Dep = _iDepartmentManager.GetDepartmentsById(model.Department.DepartmentId);
                    if (Dep != null)
                    {
                        Dep.Name = model.Department.Name;
                        Dep.Remarks = model.Department.Remarks;
                        Dep.UpdatedBy = PortalContext.CurrentUser.UserName;
                        Dep.LastUpdate = DateTime.Now;
                        model.Department = Dep;

                    }
                    else
                    {
                        model.Department.UserId = PortalContext.CurrentUser.UserName;
                        model.Department.UpdatedBy = PortalContext.CurrentUser.UserName;
                        model.Department.EntryDate = DateTime.Now;
                        model.Department.LastUpdate = DateTime.Now;
                    }

                    _iDepartmentManager.Create(model.Department);
                }

            }
            return View(model);
        }
        //public ActionResult Delete(string DepartmentId, DepartmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (DepartmentId != null)
        //        {
        //            RMS.Model.Department Dep = new RMS.Model.Department();
        //            Dep = _iDepartmentManager.GetDepartmentsById(Convert.ToInt64(DepartmentId));
        //            if (Dep != null)
        //            {
        //                _iDepartmentManager.Delete(Dep);
        //            }
        //            else
        //            {
        //                //model.SuccessMessage = 0;
        //                model.FailedMessage = "Data is not found.";
        //            }
        //        }
        //    }
        //    return View("Index",model);
        //}

        [HttpGet]
        public ActionResult Delete(string departmentId)
        {
            if (departmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                DepartmentViewModel model = new DepartmentViewModel();
                long deptId = Convert.ToInt64(departmentId);
                model.Department = _iDepartmentManager.GetDepartmentsById(deptId);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Delete(DepartmentViewModel model)
        {
            if (model.Department == null)
            {
                return HttpNotFound();
            }
            Department department = _iDepartmentManager.GetDepartmentsById(model.Department.DepartmentId);
            if (department != null)
            {
                _iDepartmentManager.DeleteDepartmentById(department);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}