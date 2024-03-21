using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;
namespace RMS.Web.Controllers
{
    public class FinancialInstallmentController : Controller
    {
        private readonly IFinancialYearManager _financialYearManager;

        public FinancialInstallmentController(IFinancialYearManager financialYearManager)
        {
            _financialYearManager = financialYearManager;
        }
        public ActionResult Index(FinancialInstallmentViewModel model)
        {
            model.FinancialYears = _financialYearManager.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(int? id,FinancialInstallmentViewModel model)
        {

            model.FinancialYear = _financialYearManager.GetFinantialInstallment(id) ?? new FinancialYear();
            return View(model);
        }
        public ActionResult SearchByKey(FinancialInstallmentViewModel model)
        {
            if (model.SearchKey == null)
            {
                model.SuccessMessage = "Please enter a word to search";
                return View("Index", model);
            }
            model.FinancialYears = _financialYearManager.GetAll().Where(x =>x.Value.ToLower().Contains(model.SearchKey.ToLower())).ToList();
            return View("Index", model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateValue(FinancialInstallmentViewModel model)
        {
            var oldData = _financialYearManager.GetFinantialInstallment(model.FinancialYear.Id);
            int result = 0;
            if (oldData != null)
            {
                oldData.Value = model.FinancialYear.Value;
                oldData.LastUpdateDate = DateTime.Now.Date;
                oldData.LastUpdateId = PortalContext.CurrentUser.UserName;
                 result = _financialYearManager.Save(oldData);
            }
            else
            {
                model.FinancialYear.CreateDate = DateTime.Now.Date;
                model.FinancialYear.CreateId = PortalContext.CurrentUser.UserName;
                 result = _financialYearManager.Save(model.FinancialYear);
            }
            if (result == 1)
            {
                model.SuccessMessage = "Data Saved Successfully!";

            }
            else
            {
                model.FailedMessage = "Data creation failed!";
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(string id, FinancialInstallmentViewModel model)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            int finantialYearId = Convert.ToInt32(id);
            int ss = _financialYearManager.DeleteFinantialYear(finantialYearId);
            if (ss == 0)
            {
                model.SuccessMessage = "Data Deleted Successfully!";

            }
            return RedirectToAction("Index",model);
        }
	}
}