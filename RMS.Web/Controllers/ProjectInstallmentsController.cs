using System.Web.Routing;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace RMS.Web.Controllers
{
    public class ProjectInstallmentsController : Controller
    {
        private readonly IProjectInstallmentManager _iProjectInstallmentManager;
        private readonly ICommonCodeManager _iCommonCodeManager;
        private readonly IDemandManager _iDemandManager;
       
        private readonly IFinancialInstallmentManager _ifinancialInstallmentManager;
        private IFinancialYearManager _iFinancialYearManager;
        public ProjectInstallmentsController(IProjectInstallmentManager iProjectInstallmentManager, ICommonCodeManager iCommonCodeManager,
            IDemandManager iDemandManager,  IFinancialInstallmentManager ifinancialInstallmentManager, IFinancialYearManager iFinancialYearManager)
        {
            _iProjectInstallmentManager = iProjectInstallmentManager;
            _iCommonCodeManager = iCommonCodeManager;
            _iDemandManager =iDemandManager;
            
            _ifinancialInstallmentManager = ifinancialInstallmentManager;
            _iFinancialYearManager = iFinancialYearManager;
        }
        public ActionResult Index(string projectID)
        {
            ProjectPaymentInstallmentViewModel model=new ProjectPaymentInstallmentViewModel();
            long projectId = Convert.ToInt64(projectID);
            List<ProjectInstallment> projectInstallments = _iProjectInstallmentManager.GetProjectInstallmentsById(projectId);
            //foreach (var item in projectInstallments)
            //{
            //    item.InstallmentName = _iCommonCodeManager.GetCommonCodeTypeValueById(Convert.ToInt64(item.InstallmentNo));
            //    item.ProjectName = _iDemandManager.GetProjectNameById(Convert.ToInt64(item.ProjectId));
            //    model.ProjectInstallments.Add(item);
            //}
            //model.ProjectInstallment.ProjectId = projectId;
            //model.ProjectInstallment.ProjectName = _iDemandManager.GetProjectNameById(Convert.ToInt64(projectID));
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(string financialID,long? projectIdenty)
        {
            ProjectPaymentInstallmentViewModel model = new ProjectPaymentInstallmentViewModel();
            long projectId = Convert.ToInt64(financialID);
            List<ProjectInstallment> projectInstallments = _iProjectInstallmentManager.GetProjectInstallmentsById(projectId);
            if(projectInstallments.Count>0)
            {
                
                int i = 0;
                foreach (var item in projectInstallments)
                {
                    i++;
                    string time = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000000000);
                    time = time + Convert.ToString(i);
                    model.Key = time;                   
                    model.ProjectInstallmentDictionary.Add(model.Key, item);
                }
            }
            else
            {
                model.ProjectInstallmentDictionary.Add("1", new ProjectInstallment());
            }
            model.FinancialInstallment.FinancialInstallmentId = projectId;
            model.Demand.DemandId = (long) projectIdenty;
            model.CommonCodes = _iCommonCodeManager.GetCommonCodeByType("Payment Installment");
            //model.ProjectInstallment.ProjectName = _iDemandManager.GetProjectNameById(Convert.ToInt64(projectID));
            return View(model);
        }
        [HttpGet]
        public ActionResult FinacialYearCreate(long? demandId, ProjectPaymentInstallmentViewModel model)
        {


            model.FinancialInstallments = _ifinancialInstallmentManager.GetFinancialInstallment(demandId);
           if (model.FinancialInstallments.Count > 0)
            {
               
                int i = 0;
                foreach (var item in model.FinancialInstallments)
                {
                    i++;
                    string time = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000000000);
                    time = time + Convert.ToString(i);
                    model.Key = time;
                    model.FinancialInstallmentDictionary.Add(model.Key, item);
                }
            }
            else
            {
                model.FinancialInstallmentDictionary.Add("1", new FinancialInstallment());
            }
            model.FinancialYears = _iFinancialYearManager.GetAll();
            model.Demand = _iDemandManager.GetDemandById(demandId);
            //model.ProjectInstallment.ProjectId = projectId;
            //model.CommonCodes = _iCommonCodeManager.GetCommonCodeByType("Payment Installment");
            //model.ProjectInstallment.ProjectName = _iDemandManager.GetProjectNameById(Convert.ToInt64(projectID));
            return View(model);
        }

        public ActionResult TransferIndex(long? installmentId, string financialId, long? projectId)
        {
            return View();
            //return RedirectToAction("Create", new { financialID = financialId, projectIdenty = projectId });
        }
        public ActionResult UpdateStatus(long? installmentId, string financialId, long? projectId, string crteDate)
        {
            DateTime? createDate = Convert.ToDateTime(crteDate);
            _iProjectInstallmentManager.UpdateStatus(installmentId,createDate);
            return RedirectToAction("Create", new { financialID = financialId, projectIdenty = projectId });
        }
        [HttpPost]
        public ActionResult Create(ProjectPaymentInstallmentViewModel model)
        {

            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                decimal instalmentAmount = 0;
                List<ProjectInstallment> projectInstallments = model.ProjectInstallmentDictionary.Select(x => x.Value).ToList();
                foreach (var projectInstallment in projectInstallments)
                {
                    //projectInstallment.ProjectId = model.ProjectInstallment.ProjectId;
                    instalmentAmount += Convert.ToDecimal(projectInstallment.Amount);
                    model.ProjectInstallments.Add(projectInstallment);
                }           
                //decimal projectAmount = _iProjectPaymentManager.GetProjectPaymentAmountById(model.ProjectInstallment.ProjectId);
                //if (instalmentAmount > projectAmount)
                //{
                //    model.Message = "Total installment amount exceeds the project budget amount";
                //}
                //else
                //{
                    _iProjectInstallmentManager.DeleteProjectInstallmentByFinancialId(model.FinancialInstallment.FinancialInstallmentId);
                    model.Message = _iProjectInstallmentManager.SaveProjectInstallments(model.ProjectInstallments, model.FinancialInstallment.FinancialInstallmentId);
                //}
                model.CommonCodes = _iCommonCodeManager.GetCommonCodeByType("Payment Installment");
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult FinacialYearCreate(ProjectPaymentInstallmentViewModel model)
        {

            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
               
                List<FinancialInstallment> financialInstallments = model.FinancialInstallmentDictionary.Select(x => x.Value).ToList();
                //foreach (var projectInstallment in projectInstallments)
                //{
                    
                //    model.ProjectInstallments.Add(projectInstallment);
                //}
          
               
                    //_ifinancialInstallmentManager.DeleteFinancialInstallmentByProjectId(model.Demand.DemandId);
               
                model.FinancialInstallments = financialInstallments;
                model.Message = _ifinancialInstallmentManager.SaveFinancialInstallments(model.FinancialInstallments, model.Demand.DemandId);

                    model.FinancialYears = _iFinancialYearManager.GetAll();
                return View(model);
            }
        }

        public ActionResult AddNewRow(ProjectPaymentInstallmentViewModel model)
        {
            model.CommonCodes = _iCommonCodeManager.GetCommonCodeByType("Payment Installment");
            model.Key = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            model.ProjectInstallmentDictionary.Add(model.Key, new ProjectInstallment());
            return PartialView("_newRow", model);
        }
        public ActionResult AddFinancialRow(ProjectPaymentInstallmentViewModel model)
        {
            model.FinancialYears = _iFinancialYearManager.GetAll();
            model.Key = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            model.FinancialInstallmentDictionary.Add(model.Key, new FinancialInstallment());
            return PartialView("_newFinancialRow", model);
        }
        [HttpGet]
        public ActionResult Edit(string projectInstallmentId)
        {
            ProjectPaymentInstallmentViewModel model = new ProjectPaymentInstallmentViewModel();
            if (projectInstallmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           
            ProjectInstallment projectInstallment = _iProjectInstallmentManager.GetProjectInstallmentById(Convert.ToInt64(projectInstallmentId));
            model.ProjectInstallment = projectInstallment;
            model.CommonCodes = _iCommonCodeManager.GetCommonCodeByType("Payment Installment");
            model.Demands = _iDemandManager.GetAll();
            //model.ProjectInstallment.ProjectId = projectInstallment.ProjectId;
            //model.ProjectInstallment.ProjectName = _iDemandManager.GetProjectNameById(projectInstallment.ProjectId);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ProjectPaymentInstallmentViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                decimal instalmentsAmount = 0;
                //decimal projectAmountFromInstallments = _iProjectInstallmentManager.GetTotalInstallmentAmountByProjectId(model.ProjectInstallment.ProjectId);
                //decimal currentInstallmentAmount = _iProjectInstallmentManager.GetInstallmentAmountById(model.ProjectInstallment.InstallmentId);
                //decimal projectAmount = _iProjectPaymentManager.GetProjectPaymentAmountById(model.ProjectInstallment.ProjectId);
                //decimal installmentsAmountExcludingCurrentInstallment = projectAmountFromInstallments - currentInstallmentAmount;
                //instalmentsAmount = Convert.ToDecimal(installmentsAmountExcludingCurrentInstallment + model.ProjectInstallment.Amount);
                //if (instalmentsAmount > projectAmount)
                //{
                //    model.Message = "Total installment amount exceeds the project budget amount";
                //}
                //else 
                //{
                //    model.Message = _iProjectInstallmentManager.SavePaymentInstallment(model.ProjectInstallment);
                //}

                //model.CommonCodes = _iCommonCodeManager.GetCommonCodeByType("Payment Installment");
                //model.Demands = _iDemandManager.GetAll();
                //model.ProjectInstallment.ProjectId = model.ProjectInstallment.ProjectId;
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Delete(long? financialID, long? projectIdenty)
        {
            try
            {
                _ifinancialInstallmentManager.DeleteFinancialInstallment(financialID);
                return RedirectToAction("FinacialYearCreate", new { demandId = projectIdenty });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
          
        }
        [HttpPost]
        public ActionResult Delete(ProjectPaymentInstallmentViewModel model)
        {
            if(model==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            _iProjectInstallmentManager.DeleteProjectInstallment(model.ProjectInstallment);
            //return RedirectToAction("Index", new { projectID = model.ProjectInstallment.ProjectId });
            return View();
        }
	}
}