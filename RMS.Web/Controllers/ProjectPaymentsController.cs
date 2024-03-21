using iTextSharp.text.pdf.qrcode;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
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
    public class ProjectPaymentsController : Controller
    {
        
        private readonly IDemandManager _iDemandManager;
        private readonly IProjectInstallmentManager _projectInstallmentManager;
        private readonly IFinancialInstallmentManager _financialInstallmentManager;

        public ProjectPaymentsController(IDemandManager iDemandManager, IProjectInstallmentManager projectInstallmentManager, IFinancialInstallmentManager financialInstallmentManager)
        {
          
            _iDemandManager = iDemandManager;
            _projectInstallmentManager = projectInstallmentManager;
            _financialInstallmentManager = financialInstallmentManager;
            
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create(long? id,ProjectPaymentViewModel model)
        {
       
            model.Demands = _iDemandManager.GetAll();
            model.FinancialInstallment = _iDemandManager.GetFinanceById(id) ?? new FinancialInstallment();
            model.Amount = _projectInstallmentManager.GetProjectAmount(id) ?? new decimal() ;
            model.FinancialInstallment.ProjectId =id != null? (long) id: 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProjectPaymentViewModel model)
        {
            model.BGPG = model.FinancialInstallment.BGPG;
            model.BGPG.FinancialInstallmentId = model.FinancialInstallment.FinancialInstallmentId;
            model.Message = _financialInstallmentManager.SaveBgpg(model.BGPG);
            model.Demands = _iDemandManager.GetAll();
            return View(model);
        }

    }
}