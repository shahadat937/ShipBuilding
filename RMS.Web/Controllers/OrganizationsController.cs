using RMS.BLL.IManager;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationManager _iOrganizationManager;
        public OrganizationsController(IOrganizationManager iOrganizationManager)
        {
            _iOrganizationManager = iOrganizationManager;
        }
        public ActionResult Index(OrganizationViewModel model)
        {
            model.Organizations = _iOrganizationManager.GetAllOrganizations();
            return View(model);
        }
        public ActionResult SearchByKey( OrganizationViewModel model)
        {
            if(model.SearchKey==null)
            {
                model.Message = "Please enter a word to search";
                return View("Index", model);
            }
            string searchKey = model.SearchKey;
            model.Organizations = _iOrganizationManager.GetOrganizationsBySearchKey(searchKey);
            return View("Index",model);
        }
        [HttpGet]
        public ActionResult Create(OrganizationViewModel model)
        {
            return View(model);
        }
        [HttpPost, ActionName("Create")]
        public ActionResult SaveCreate(OrganizationViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Message = _iOrganizationManager.SaveOrganization(model.Organization);
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(string id, OrganizationViewModel model)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            long organizationId = Convert.ToInt64(id);
            model.Organization = _iOrganizationManager.GetOrganizationById(organizationId);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(OrganizationViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Message = _iOrganizationManager.EditOrganization(model.Organization);
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(string id,OrganizationViewModel model)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            long organizationId = Convert.ToInt64(id);
            model.Organization = _iOrganizationManager.GetOrganizationById(organizationId);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(OrganizationViewModel model)
        {
            if(model==null)
            {
                return HttpNotFound();
            }
            _iOrganizationManager.DeleteOrganization(model.Organization);
            return RedirectToAction("Index");
        }
    }
}