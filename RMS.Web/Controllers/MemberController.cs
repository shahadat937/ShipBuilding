using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberManager _memberManager;
        private readonly ICommiteeManager _comManager;
        private readonly ICommonCodeManager _commonCodeManager;
        public MemberController(IMemberManager memberManager, ICommiteeManager comManager, ICommonCodeManager commonCodeManager)
        {

            _memberManager = memberManager;
            _comManager = comManager;
            _commonCodeManager = commonCodeManager;


        }
        public ActionResult Index(MemberViewModel model)
        {
            model.Members = _memberManager.GetMember();
            return View(model);
        }
        public ActionResult CIndex(CommiteeViewModel model)
        {
            //model.Commitees = _comManager.GetCommitee();
            return View(model);
        }
        //public ActionResult SearchByKey(MemberViewModel model)
        //{
        //    string searchKey = model.Member.MemberName;
        //    if (searchKey == null)
        //    {
        //        model.Members = _memberManager.GetMember();
        //    }
        //    else
        //    {
        //        searchKey = searchKey.ToLower();
        //        model.Members = _memberManager.GetMember().Where(x => x.MemberName.ToLower().Contains(searchKey)).ToList();
        //        if (!model.Members.Any())
        //        {
        //            //model.SuccessMessage = 0;
        //            model.FailedMessage = "Data is not found.";
        //        }
        //    }
        //    return View("Index", model);
        //}
        public ActionResult SearchByKey(MemberViewModel model)
        {
            string searchKey = model.SearchKey;
            if (searchKey == null)
            {
                model.FailedMessage = "Please enter a keyword to search";
                model.Members = _memberManager.GetMember();
            }
            else
            {
                searchKey = searchKey.ToLower();
                model.Members = _memberManager.GetAllMembersBySearchKeyword(searchKey);
                if (!model.Members.Any())
                {
                    //model.SuccessMessage = 0;
                    model.FailedMessage = "Data is not found with this " + "'" + searchKey + "' keyword";
                }
            }
            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Create(long? MemberId, MemberViewModel model)
        {
            model.Member = _memberManager.GetMemberById(MemberId) ?? new Member();
            model.RankLists = _commonCodeManager.GetCommonCodeByType("Rank").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {

                    var imageUrl = "";
                    string file = "";
                    var validImageTypes = new string[]
                    {
                        "image/gif",
                        "image/jpeg",
                        "image/pjpeg",
                        "image/png"
                    };

                    if (model.Image != null && model.Image.ContentLength <= 6000*1024)
                    {
                        Stream stream = model.Image.InputStream;
                        System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

                        int height = image.Height;
                        int width = image.Width;
                        //if (height == 300 && width == 300)
                        //{
                        var uploadDir = "~/uploads";
                        file = "Image_Photo_ " + PortalContext.CurrentUser.UserName + "_" + model.Image.FileName;
                        var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
                        imageUrl = Path.Combine(uploadDir, file);
                        model.Image.SaveAs(imagePath);

                        //}

                    }
                    else
                    {
                        model.FailedMessage = "Please Select image Less then 6 MB!";
                    }
                    RMS.Model.Member Mem = new RMS.Model.Member();
                    Mem = _memberManager.GetMemberById(model.Member.MemberId);
                    if (Mem != null)
                    {
                        Mem.OfficeId = model.Member.OfficeId;
                        Mem.MemberName = model.Member.MemberName;
                        if (imageUrl.Length > 1)
                        {
                            Mem.ImageUrl = imageUrl;
                        }
                        Mem.Email = model.Member.Email;
                        Mem.PhoneNo = model.Member.PhoneNo;
                        Mem.Rank = model.Member.Rank;
                        Mem.UpdatedBy = PortalContext.CurrentUser.UserName;
                        Mem.LastUpdate = DateTime.Now;
                        model.Member = Mem;

                    }
                    else
                    {
                        model.Member.ImageUrl = imageUrl;
                        model.Member.UserId = PortalContext.CurrentUser.UserName;
                        model.Member.UpdatedBy = PortalContext.CurrentUser.UserName;
                        model.Member.EntryDate = DateTime.Now;
                        model.Member.LastUpdate = DateTime.Now;
                    }
                    int result = _memberManager.Create(model.Member);

                    if (result == 1)
                    {
                        model.SuccessMessage = "Member has been created successfully!";

                    }
                    else
                    {
                        model.FailedMessage = "Member creation failed!";
                    }
                }
            }
            model.RankLists = _commonCodeManager.GetCommonCodeByType("Rank").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View(model);
        }
        public FileResult Download(string FileID)
        {
            int CurrentFileID = Convert.ToInt32(FileID);
            var filesCol = _memberManager.GetMemberById(CurrentFileID).ImageUrl;
            string CurrentFileName = Path.Combine(Server.MapPath(filesCol.ToString()));

            string contentType = string.Empty;

            if (CurrentFileName.Contains(".jpg"))
            {
                contentType = "Image/jpg";
            }
            else if (CurrentFileName.Contains(".JPG"))
            {
                contentType = "Image/JPG";
            }
            else if (CurrentFileName.Contains(".JPEG"))
            {
                contentType = "Image/JPEG";
            }
            else if (CurrentFileName.Contains(".png"))
            {
                contentType = "Image/png";
            }
            else if (CurrentFileName.Contains(".gif"))
            {
                contentType = "Image/gif";
            }
            return File(CurrentFileName, contentType, filesCol);
        }  
    }
}