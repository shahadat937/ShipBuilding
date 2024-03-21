using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using iTextSharp.text.pdf.qrcode;
using RMS.BLL.IManager;
using RMS.Utility;
using RMS.Web.Models;
using RMS.Model;
using HasCode;
using RMS.Web.Models.ViewModel;
using System.Text.RegularExpressions;

namespace RMS.Web.Controllers
{

   // [Authorize]
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IUserURLRightManager _userURLRightManager;
        private IUserManager _userManager;
        private IProjectFinalStatuManager _iProjectFinalStatuManager;
        private IDemandManager _demandManager;
        private IFlowSetupManager _flowSetupManager;
        private IContractFileManager _contractFileManager;
        private IProjectNoteManager _projectNoteManager;
        public HomeController(IProjectNoteManager _projectNoteManager, IUserManager userManager, IUserURLRightManager userURLRightManager, IProjectFinalStatuManager iProjectFinalStatuManager, IDemandManager demandManager, IFlowSetupManager flowSetupManager, IContractFileManager contractFileManager)
        {
            _userManager = userManager;
            _userURLRightManager = userURLRightManager;
            _iProjectFinalStatuManager = iProjectFinalStatuManager;
            _demandManager = demandManager;
            _flowSetupManager = flowSetupManager;
            _contractFileManager = contractFileManager;
            this._projectNoteManager = _projectNoteManager;
        }
         [SessionExpireFilter]
        public async Task<ActionResult> Index(ProjectViewModel model)
        {

            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (PortalContext.CurrentUser.IsValidate)
                {
          
                    model.ProjectFinalStatus = _flowSetupManager.GetAll();
                    var demand = _demandManager.GetAll().ToList();
                    model.ProjectCount = demand.Count.ToString();
                    model.AllProjectStatus = _demandManager.GetProjectStatusByYear();
       
                    model.CompleteProject = _demandManager.GetCompleteProject().Count.ToString();
                    model.OngoingProject = (demand.Count(x => x.ProjectType == 6) - Convert.ToInt16(model.CompleteProject)).ToString();
                       //    demands.ForEach(x => items.Add(new SelectListItem() { Text = x.FileNo, Value = x.DemandId.ToString() }));
                    //model.CutrrentProject = model.ProjectFinalStatus.Count(x => x.ProjectType == 7).ToString();
                    //model.OngoingProject = model.ProjectFinalStatus.Count(x => x.ProjectType == 6).ToString();
             
                    foreach (var dd in demand)
                    {
                       ProjectStatu ppst = new ProjectStatu();
                        ProjectStatu pendinginfo = new ProjectStatu();
                      
                       var ss = _flowSetupManager.GetPendingDays(dd.DemandId);
                       PendingDepartmentStatus_Result PendingDays = await ss;
                        if (PendingDays != null)
                        {
                            DateTime da = (DateTime) PendingDays.DepEndDate;
                            if ((DateTime.Now - da).TotalDays > 0)
                            {
                                pendinginfo.ProjectName = dd.FileNo;
                                pendinginfo.DeptName = PendingDays.DepartmentId;
                                pendinginfo.PendingDays = DateTime.Now.Date.Subtract(da.Date).TotalDays.ToString() ;
                                model.PendingInfoes.Add(pendinginfo);
                            }
                        }
           
                        float totalDone = 0;
                        int total = 0;
                        if (dd.DemandId > 0)
                        {
                          
                            int ProjectId = Convert.ToInt32(dd.DemandId);
                            var FlowList = _flowSetupManager.GetFlowSetupByFlowId(ProjectId).OrderBy(x => x.FlowSerial);
                            foreach (var flowSetup in FlowList)
                            {
                                if (flowSetup.IsSkip != true)
                                {
                                    total += 1;
                                    string[] com = flowSetup.IsComplete.Split(',');
                                    string count = null;
                                    foreach (var item in com)
                                    {
                                        if (item.ToLower() != "false") count = "True";
                                        else
                                        {
                                            count = "False";
                                            break;
                                        }
                                    }
                                    if (count == "True") totalDone += 1;
                                }
                            }
                              ppst.ProjectName = dd.FileNo;
                            ppst.ProjectId = dd.DemandId;
                            double result = (double)totalDone / total;
                            if (totalDone > 0) ppst.CompleteMileStone = (int)(result * 100);
                            ppst.ProjectType = dd.ProjectType.ToString();
                            ppst.IsComplete = dd.IsComplete;
                            ppst.Complete = _projectNoteManager.GetCompleteStatus(dd.DemandId);
                            model.ProjectStatus.Add(ppst);
                        }
                    }
                  
                    return View(model);
                    // Rifat End

                    //return View();
                }
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult LoginTest()
        {
            return View();
        }
         public JsonResult ProjectStatusForPicChart()
         {
             ProjectStatusForGanttChart projectStatusForGanttCharts =new ProjectStatusForGanttChart();
             var dd = _demandManager.GetAll().ToList();
             projectStatusForGanttCharts.TotalProject = dd.Count();
             projectStatusForGanttCharts.CompleteProject = _demandManager.GetCompleteProject().Count();
             projectStatusForGanttCharts.OngoingProject = (dd.Count(x => x.ProjectType == 6) - Convert.ToInt16(projectStatusForGanttCharts.CompleteProject));

             return Json(projectStatusForGanttCharts, JsonRequestBehavior.AllowGet);
         }
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                string msg = "";
                var sc = new IBSHasCode();
                string Pass = "";
                Pass = sc.CreateDoubleHas(model.UserName, model.Password);
                if (ModelState.IsValid)
                {
                    var sd = _userManager.FindUser(model.UserName);
                    if (sd.Count > 0)
                    {
                        if (Membership.ValidateUser(model.UserName, model.Password))
                        {
                            FormsAuthentication.SetAuthCookie(model.UserName, false);
                            //PortalContext.CurrentUser =new  PortalUser();
                            //PortalContext.CurrentUser.IsValidate = true;
                            //PortalContext.CurrentUser.UserName = model.UserName;
                            var authTicket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now,
                                DateTime.Now.AddMinutes(20), true, "", "/");

                            //encrypt the ticket and add it to a cookie
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                            Response.Cookies.Add(cookie);

                            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                        }
                        else
                        {
                            TempData["Msg"] = "Invalid User or Password.";
                            return RedirectToAction("Login", "Account");
                        }
                    }
                }
                TempData["Msg"] = "Invalid User or Password.";
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "System Error."+ex.Message;
                return RedirectToAction("Login", "Account");
            }
            
        }
        public ActionResult ForgotPassword()
        {
            var model = new UserViewModel();
            return View("ForgotPassword", model);
        }
        //[Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpPost]
        public ActionResult ForgotPassword(UserViewModel model)
        {
            if (model.User.UserName != null && model.User.PhoneNumber != null)
            {
                string UserN = _userManager.FindOne(model.User.UserName).UserName;
                string UserPhone = _userManager.FindOne(model.User.UserName).PhoneNumber;
                if (UserN == model.User.UserName & UserPhone == model.User.PhoneNumber)
                {
                    if (model.User.Password != null)
                    {
                        string password = model.User.Password;
                        string oldPass = _userManager.FindOne(model.User.UserName).Password;
                        if (IsAlphaNumeric(password) || IsRegularExpression(password))
                        {
                            ResponseModel = _userManager.ForgotPassword(oldPass, model.User);
                            model.IsSucceed = ResponseModel.ResponsStatus;
                            model.Message = ResponseModel.Message;
                            if (model.IsSucceed == 1)
                            {
                                //FormsAuthentication.SignOut();
                                //Session.Clear();

                                return RedirectToAction("Login", "Account");
                            }
                        }
                        else
                        {
                            model.Message =
                                "Password must be combination of alphanumeric with An uppercase letter & a lowercase letter.";

                        }

                    }
                }
                else
                {
                    model.Message = "User Id & Phone Number Not Match.";
                }
            }
            return View("ForgotPassword", model);
        }
        public static bool IsAlphaNumeric(string s)
        {
            var rg = new Regex(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]{8,30}$");
            return rg.IsMatch(s);
        }

        public static bool IsRegularExpression(string s)
        {
            var rg = new Regex(@"^(?=.*[!@#$%^&*()\-_=+`~\[\]{}?|])(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,20}$");
            return rg.IsMatch(s);

        }

        private string CheckADUser(string userName, string password)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "pbldc-ad.pbl.com"))
            {
                string ret = "";
                try
                {
                    bool isValid = pc.ValidateCredentials(userName, password);
                    if (isValid)
                    {
                        ret = "Y";
                    }
                    else
                    {
                        ret = "N";
                    }
                }
                catch (Exception ex)
                {

                    ret = "N";
                }
                return ret;
            }
            //string ret = "";
            //string domain = "pbl";
            //string ldapPath = "LDAP://pbldc-ad.pbl.com/DC=pbl,DC=com";
            //string domainAndUserName = domain + @"\" + userName;
            //var entry = new DirectoryEntry(ldapPath, domainAndUserName, password);
            //var search = new DirectorySearcher(entry);
            //search.Filter = "(SAMAccountName = " + userName + ")";
            //search.PropertiesToLoad.Add("cn");
            //try
            //{
            //    SearchResult result = search.FindOne();
            //    ret = result == null ? "N" : "Y";
            //}
            //catch (Exception ex)
            //{

            //    ret = "N";
            //}
            //return ret;
        }
        //private string GetIP(string machineName)
        //{

        //    IPHostEntry ipEntry = Dns.GetHostEntry(machineName);
        //    IPAddress[] addr = ipEntry.AddressList;
        //    return addr[addr.Length - 1].ToString();
        //}
        //public static PhysicalAddress[] StoreNetworkInterfaceAddresses()
        //{
        //    IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
        //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        //    if (nics == null || nics.Length < 1)
        //    {
        //        Console.WriteLine("  No network interfaces found.");
        //        return null;
        //    }
        //    PhysicalAddress[] addresses = new PhysicalAddress[nics.Length];
        //    int i = 0;
        //    foreach (NetworkInterface adapter in nics)
        //    {
        //        IPInterfaceProperties properties = adapter.GetIPProperties();
        //        PhysicalAddress address = adapter.GetPhysicalAddress();
        //        byte[] bytes = address.GetAddressBytes();
        //        PhysicalAddress newAddress = new PhysicalAddress(bytes);
        //        addresses[i++] = newAddress;
        //    }
        //    return addresses;
        //}


        


        public JsonResult CheckExistingDepartment(string department)
        {
            bool ifEmailExist = false;
            try
            {
                //ifEmailExist = UserEmailAddress.Equals("mukeshknayak@gmail.com") ? true : false;
                return Json(!ifEmailExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Menu()
        {
            PortalUser portalUser = PortalContext.CurrentUser;
            if (portalUser.IsValidate)
            {
                var userRightList = _userURLRightManager.GetUserRightTreeView(portalUser.UserRole);
                return PartialView("_menu", userRightList);
            }
            else
            {
                TempData["Msg"] = "Invalid User.";
                return RedirectToAction("Login", "Account");
            }
            //return Redirect(Url.Action("Login", "Home"));
            //return RedirectToAction("Login", "Home");
        }
        public ActionResult Notification()
        {
            if (PortalContext.CurrentUser.IsValidate == false)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var notification = new NotificationCustomModel();
                //notification = _vwNotificationManager.GetNotification();
                //if (notification != null)
                //{
                //    ViewBag.count = notification.RunningHour + notification.NextDockingFrom + notification.NextRefitFrom + notification.MejorDemand + notification.SignalInfo;
                //}
                //else
                //{
                //    notification = new NotificationCustomModel
                //    {
                //        Count = 0,
                //        RunningHour = 0,
                //        NextDockingFrom = 0,
                //        NextRefitFrom = 0,
                //        MejorDemand = 0,
                //        SignalInfo = 0
                //    };
                //}
                return PartialView("_Notification", notification);
            }
        }
        [HttpGet]
        public ActionResult DashboardIndex(UserViewModel model)
        {
            var notification = new NotificationCustomModel();
            //notification = _vwNotificationManager.GetNotification();
            //ViewBag.count = notification.RunningHour + notification.NextDockingFrom + notification.NextRefitFrom + notification.MejorDemand + notification.SignalInfo;
            //model.NumberOfNotification = ViewBag.count;

            return View(model);
        }

        
    }
}