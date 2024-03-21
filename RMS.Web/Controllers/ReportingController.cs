using System;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class ReportingController : Controller
    {
        //RM_AGBEntities db = new RM_AGBEntities();
        private readonly IReportingManager _reportingManager;
        private ICommonCodeManager _commonCodeManager;
        public ReportingController(IReportingManager reportingManager, ICommonCodeManager commonCodeManager)
        {
            _reportingManager = reportingManager;
            _commonCodeManager = commonCodeManager;
        }
        public ActionResult Index(ReportingViewModel model)
        {
            //model.Ranks = _commonCodeManager.GetCommonCodeByType("RANK");
            //model.NameOfCourses = _commonCodeManager.GetCommonCodeByType("NameofCourse");
            model.ReportingTreeViews = _reportingManager.GetReportingTeeView();
            return View(model);
        }

        public ActionResult ReportView(ReportingViewModel model)
        {
            //string BankCode = PortalContext.CurrentUser.BankCode.Trim();

            //model.ExchangeCompany = _reportingManager.ExchangeCompany();
            //model.ShipInfoes = _controlShipInfoManager.Totalship();
            //model.ShipInfoes = _controlShipInfoManager.ShipBranchInfo();
            //model.ShipNameInfoes = _controlShipInfoManager.FMsgShipBankInfo();
            Reporting reportingObj = _reportingManager.GetReportParameterBySlNo(model.SlNo) ?? new Reporting();
            model.ReportName = reportingObj.ReportName;
            model.Para1 = reportingObj.Para1;
            model.Para2 = reportingObj.Para2;
            model.Para3 = reportingObj.Para3;
            model.Para4 = reportingObj.Para4;
            if (!model.Para1.Equals("0"))
            {
                model.Parameters.Add(model.Para1, "");
            }
            if (!model.Para2.Equals("0"))
            {
                model.Parameters.Add(model.Para2, "");
            }
            if (!model.Para3.Equals("0"))
            {
                model.Parameters.Add(model.Para3, "");
            }
            if (!model.Para4.Equals("0"))
            {
                model.Parameters.Add(model.Para4, "");
            }
            model.SlNo = model.SlNo;
            return View(model);
        }

        public ActionResult AspReport(ReportingViewModel model)
        {
            if (Session["paramertes"] == null)
            {
                Session["paramertes"] = model.Parameters;
                Session["slNo"] = model.SlNo;
            }

            return Redirect("~/Reports/TreeReportViwer.aspx");
        }
        //public ActionResult RMSReport(RemittanceViewModel model)
        //{
        //    DateTime fromDate = Convert.ToDateTime(model.FromDate);
        //    DateTime toDate = Convert.ToDateTime(model.ToDate);
        //    //Response.Redirect(@"~/ASPReport/aspReportView.aspx?id=" + Id);
        //    return Redirect("~/Reports/FrmReportViewer.aspx?dateFrom="+fromDate+"?dateTo="+toDate);
        //}

    }
    //[Authorize]
    //public class ReportingController : Controller
    //{

    //    private readonly IReportingManager _reportingManager;
    //    private IBranchInfoManager _branchInfoManager;
    //    private ICommonCodeManager _commonCodeManager;
    //    public ReportingController(IReportingManager reportingManager, IBranchInfoManager branchInfoManager, ICommonCodeManager commonCodeManager)
    //    {
    //        _reportingManager = reportingManager;
    //        _branchInfoManager = branchInfoManager;
    //        _commonCodeManager = commonCodeManager;
    //    }
    //    [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin,BranchUser")]
    //    public ActionResult Index(ReportingViewModel model)
    //    {
    //        model.ReportingTreeViews = _reportingManager.GetReportingTeeView();
    //        return View(model);
    //    }
    //    [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin,BranchUser")]
    //    public ActionResult ReportView(string slNo, ReportingViewModel model)
    //    {
    //        model.Exchanges = _branchInfoManager.ExchangeHouseList();
    //        model.BankInfos = _branchInfoManager.GetBankList();
    //        model.Districts = _branchInfoManager.GetDistrictList(PortalContext.CurrentUser.BankCode);
    //        model.BranchInfos = _branchInfoManager.GetBranchListByBankCode();
    //        model.RemittanceStatusList = _commonCodeManager.GetByType("RemittanceStatus");
    //        model.IssuePaymentList = _commonCodeManager.GetByType("IssuePaymentCode");
    //        model.Reporting.SlNo = slNo;
    //        //Session["slNo"] = slNo;
    //        Reporting reportingObj = _reportingManager.GetReportParameterBySlNo(slNo) ?? new Reporting();
    //        model.Reporting.Para1 = reportingObj.Para1;
    //        model.Reporting.Para2 = reportingObj.Para2;
    //        model.Reporting.Para3 = reportingObj.Para3;
    //        model.Reporting.Para4 = reportingObj.Para4;
    //        model.Reporting.ReportName = reportingObj.ReportName;
    //        if (!model.Reporting.Para1.Equals("0"))
    //        {
    //            model.Parameters.Add(model.Reporting.Para1, "");
    //        }
    //        if (!model.Reporting.Para2.Equals("0"))
    //        {
    //            model.Parameters.Add(model.Reporting.Para2, "");
    //        }
    //        if (!model.Reporting.Para3.Equals("0"))
    //        {
    //            model.Parameters.Add(model.Reporting.Para3, "");
    //        }
    //        if (!model.Reporting.Para4.Equals("0"))
    //        {
    //            model.Parameters.Add(model.Reporting.Para4, "");
    //        }
    //        model.Reporting.SlNo = model.Reporting.SlNo;
    //        return View(model);
    //    }
    //    [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin,BranchUser")]
    //    public ActionResult AspReport(ReportingViewModel model)
    //    {
    //        if (Session["paramertes"] == null)
    //        {
    //            Session["paramertes"] = model.Parameters;
    //            Session["slNo"] = model.Reporting.SlNo;
    //        }

    //        return Redirect("~/Reports/TreeReportViwer.aspx");
    //    }

    //}
}