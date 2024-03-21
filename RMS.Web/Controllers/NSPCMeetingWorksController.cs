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
    public class NSPCMeetingWorksController : Controller
    {
        private readonly INSPCMeetingWorkManager _iNSPCMeetingWorkManager;
        private readonly ICommonCodeManager _iCommonCodeManager;
        private readonly IMemberManager _iMemberManager;
        public NSPCMeetingWorksController(INSPCMeetingWorkManager iNSPCMeetingWorkManager,
            ICommonCodeManager iCommonCodeManager, IMemberManager iMemberManager)
        {
            _iNSPCMeetingWorkManager = iNSPCMeetingWorkManager;
            _iCommonCodeManager = iCommonCodeManager;
            _iMemberManager = iMemberManager;
        }
        public ActionResult Index(NSPCMeetingWorkViewModel model)
        {
            model.NSPCMeetingWorks = _iNSPCMeetingWorkManager.GetAllNSPCMeetingWork();
            foreach (var item in model.NSPCMeetingWorks)
            {
                item.CategoryName = _iCommonCodeManager.GetCommonCode().First(x => x.CommonCodeID ==item.CategoryId).TypeValue;
            }
            return View(model);
        }
        public ActionResult SearchByKey(NSPCMeetingWorkViewModel model)
        {
            string searchKey = model.SearchKey;

            if(searchKey==null)
            {
                model.Message = "Please search by giving search data";
                return View("Index", model);
            }
            List<NSPCMeetingWork> nspcMeetingWorks=_iNSPCMeetingWorkManager.GetNSPCMeetingWorksBySearchKey(searchKey);
            foreach (var nspcMeetingWork in nspcMeetingWorks)
            {
                nspcMeetingWork.CategoryName = _iCommonCodeManager.GetCommonCodeTypeValueById(nspcMeetingWork.CategoryId);
                model.NSPCMeetingWorks.Add(nspcMeetingWork);
            }
            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Create(NSPCMeetingWorkViewModel model)
        {
            model.CommonCodeSelectedListItems = CommonCodeSelectedItems();
            return View(model);
        }
        [HttpPost,ActionName("Create")]
        public ActionResult SaveCreate(NSPCMeetingWorkViewModel model)
        {
            if(model.NSPCMeetingWork==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NSPCMeetingWork nspcMeetingWork = model.NSPCMeetingWork;
            model.Message = _iNSPCMeetingWorkManager.SaveNSPCMeetingWork(nspcMeetingWork);
            model.CommonCodeSelectedListItems = CommonCodeSelectedItems();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(string NSPCMeetingWorkId,NSPCMeetingWorkViewModel model)
        {
            if(NSPCMeetingWorkId==null)
            {
                return HttpNotFound();
            }
            long id = Convert.ToInt64(NSPCMeetingWorkId);
            NSPCMeetingWork nspcMeetingWork = _iNSPCMeetingWorkManager.GetNSPCMeetingWorkById(id);
            model.CommonCodeSelectedListItems = CommonCodeSelectedItems();
            model.NSPCMeetingWork = nspcMeetingWork;
            return View(model);
        }
        [HttpPost,ActionName("Edit")]
        public ActionResult Edit(NSPCMeetingWorkViewModel model)
        {
            if(model.NSPCMeetingWork==null)
            {
                return HttpNotFound();
            }
            NSPCMeetingWork nspcMeetingWork = model.NSPCMeetingWork;
            model.Message = _iNSPCMeetingWorkManager.EditNSPCMeetingWork(nspcMeetingWork);
            model.CommonCodeSelectedListItems = CommonCodeSelectedItems();
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(string nspcMeetingWorkId,NSPCMeetingWorkViewModel model)
        {
            if(nspcMeetingWorkId==null)
            {
                return HttpNotFound();
            }
            long id=Convert.ToInt64(nspcMeetingWorkId);
            NSPCMeetingWork nspcMeetingWork = _iNSPCMeetingWorkManager.GetNSPCMeetingWorkById(id);
            model.NSPCMeetingWork=nspcMeetingWork;
            string CategoryName = _iCommonCodeManager.GetCommonCode().First(x => x.CommonCodeID == nspcMeetingWork.CategoryId).TypeValue;
            model.NSPCMeetingWork.CategoryName = CategoryName;
            return View(model);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmedDelete(NSPCMeetingWorkViewModel model)
        {
            if(model.NSPCMeetingWork==null)
            {
                return HttpNotFound();
            }
            NSPCMeetingWork nspcMeetingWork =_iNSPCMeetingWorkManager.GetNSPCMeetingWorkById(model.NSPCMeetingWork.NSPCMeetingWorkId);
            int result = _iNSPCMeetingWorkManager.DeleteNSPCMeetingWork(nspcMeetingWork);
            if(result>0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public List<SelectListItem>CommonCodeSelectedItems()
        {
            NSPCMeetingWorkViewModel model = new NSPCMeetingWorkViewModel();
            List<SelectListItem> selectedItems = new List<SelectListItem>();
            List<CommonCode> commonCodes = _iCommonCodeManager.GetCommonCode();
            commonCodes.ForEach(x => selectedItems.Add(new SelectListItem() { Text = x.TypeValue, Value = x.CommonCodeID.ToString() }));
            model.CommonCodeSelectedListItems = selectedItems;
            return model.CommonCodeSelectedListItems;
        }
        public JsonResult AutoSearchResult(string prefix)
        {
            List<Member> memberList = _iMemberManager.GetMember();
            var members = from N in memberList where N.MemberName.ToLower().StartsWith(prefix.ToLower()) select new { N.MemberName };
            return Json(members, JsonRequestBehavior.AllowGet);
        }
	}
}