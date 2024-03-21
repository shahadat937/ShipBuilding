using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class TenderController : Controller
    {
        private readonly ITenderOfferManager _tenderOfferManager;
        private readonly ITenderSecificationManager  _tenderSecificationManager;
        private readonly IDemandManager _demandManager;
        private readonly IFlowSetupManager _flowSetupManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IFieldValueBackManager _fieldValueBackManager;
        public TenderController(IFieldValueBackManager fieldValueBackManager,ITenderOfferManager tenderOfferManager, ITenderSecificationManager tenderSecificationManager, IDemandManager demandManager, IFlowSetupManager flowSetupManager, ICommonCodeManager commonCodeManager)
        {
            _tenderOfferManager = tenderOfferManager;
            _tenderSecificationManager = tenderSecificationManager;
            _demandManager = demandManager;
            _flowSetupManager = flowSetupManager;
            _commonCodeManager = commonCodeManager;
            _fieldValueBackManager = fieldValueBackManager;

        }
        public ActionResult Index(string fileId, string flowserial,TenderViewModel model)
        {
            model.TenderSecifications = _tenderSecificationManager.GetAllByFileId(fileId);
            if (fileId != null)
            {
                model.TenderSecification.FileNo = Convert.ToInt64(fileId);
                model.FlowSerial = flowserial;
            }
            return View(model);
        }
        public ActionResult SearchByKey(TenderViewModel model)
        {
            string searchKey = model.SearchKey;

            searchKey = searchKey.ToLower();
            model.TenderSecifications =
                _tenderSecificationManager.GetSearchData(searchKey);
            if (!model.TenderSecifications.Any())
            {
                //model.SuccessMessage = 0;
                model.FailedMessage = "Data is not found.";
            }

            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Create(string fileId, string flowserial, long? TenderId, TenderViewModel tenderViewModel)
        {
           

            tenderViewModel.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            tenderViewModel.TenderingType = _commonCodeManager.GetCommonCodeByType("TenderingType").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            tenderViewModel.TenderSecification = _tenderSecificationManager.GettenderSecificationByFile(fileId) ?? new TenderSpecification();
            List<TenderSpecificationChild> tenderFields = _tenderSecificationManager.GetAllTenderChildByDemandId(fileId);
            if (tenderFields.Count > 0)
            {
                int i = 0;
                foreach (var item in tenderFields)
                {
                    i++;
                    string time = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000000000);
                    time = time + Convert.ToString(i);
                    tenderViewModel.Key = time;
                    tenderViewModel.TenderChildDictionary.Add(tenderViewModel.Key, item);
                }
            }
            else
            {
                tenderViewModel.TenderChildDictionary.Add("1", new TenderSpecificationChild());
            }

            if (fileId != null)
            {
                tenderViewModel.TenderSecification.FileNo = Convert.ToInt64(fileId);
                tenderViewModel.FlowSerial = flowserial;
            }

            return View(tenderViewModel);
        }
        [HttpGet]
        public ActionResult TenderApproveIndex(string fileId, string flowserial, long? TenderId, TenderViewModel tenderViewModel)
        {

            tenderViewModel.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            tenderViewModel.TenderSecification = _tenderSecificationManager.GettenderSecificationByFile(fileId) ?? new TenderSpecification();

            if (fileId != null)
            {
                tenderViewModel.TenderSecification.FileNo = Convert.ToInt64(fileId);
                tenderViewModel.FlowSerial = flowserial;
            }

            return View(tenderViewModel);
        }

        [HttpPost]
        public ActionResult Create(TenderViewModel model)
        {
            if (model.ImageUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.TenderSecification.FileUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
            }
            if (model.ApproveDocument != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.TenderSecification.ApproveUrl = ImageUpload.ImageUploadFile(model.ApproveDocument, dirFullPath, dirPath);
            }
            // Tender Child Save 

            List<TenderSpecificationChild> tenderChilds = model.TenderChildDictionary.Select(x => x.Value).ToList();
            _tenderSecificationManager.SaveTederChild(tenderChilds, model.TenderSecification.FileNo);

            var fileIdenti = model.TenderSecification.FileNo;
            var flowSerial = model.FlowSerial;
            if (model != null)
            {
                TenderSpecification com = new TenderSpecification();
                com = _tenderSecificationManager.GetTenderSpecification(model.TenderSecification.TenderIdentity);
                if (com != null)
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSecification.FileNo, model.FlowSerial, FormInformation.TenderSpecification,null);
                    _fieldValueBackManager.Save(model.TenderSecification.FileNo,"TenderIssueDate",model.TenderSecification.CreateDate.ToString());

                    com.TenderIdentity = model.TenderSecification.TenderIdentity;
                    com.Title = model.TenderSecification.Title;
                    com.CreateDate = model.TenderSecification.CreateDate;
                    com.TenderType = model.TenderSecification.TenderType;
                    //com.IsApproved = model.TenderSecification.IsApproved;
                    com.Remarks = model.TenderSecification.Remarks;
                    com.FileUrl = model.TenderSecification.FileUrl ?? com.FileUrl;
                    com.LastUpdateId = PortalContext.CurrentUser.UserName;
                    com.LastUpdateDate = DateTime.Now;
                    model.TenderSecification = com;

                }
                else
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSecification.FileNo, model.FlowSerial, FormInformation.TenderSpecification,DateTime.Now);
                    model.TenderSecification.CreateId = PortalContext.CurrentUser.UserName;
                    
                }
                int result = _tenderSecificationManager.Create(model.TenderSecification);

                if (result == 1)
                {
                    model.SuccessMessage = "Tender has been created successfully!";

                }
                else
                {
                    model.FailedMessage = "Tender creation failed!";
                }

            }
            model.TenderingType = _commonCodeManager.GetCommonCodeByType("TenderingType").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            return View("Create", model);
        }
    [HttpGet]
        public ActionResult TenderInfo(string fileId, string flowserial, long? TenderId, TenderViewModel model)
        {
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.TenderingType = _commonCodeManager.GetCommonCodeByType("TenderingType").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            if (fileId != null)
            {
                model.TenderSecification.FileNo = Convert.ToInt64(fileId);
                model.FlowSerial = flowserial;
            }
            return View(model);
        }

        public ActionResult TenderTypeSave(TenderViewModel model)
        {

     
            if (model != null)
            {
                TenderSpecification com = new TenderSpecification();
                com = _tenderSecificationManager.GetTenderType(model.TenderSecification.FileNo);
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.TenderSecification.TenderTypeFile = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                if (com != null)
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSecification.FileNo, model.FlowSerial, FormInformation.TenderDGDP, null);
                    
                    com.TenderType = model.TenderSecification.TenderType;
                    com.TenderTypeFile = model.TenderSecification.TenderTypeFile ?? com.TenderTypeFile;
                    model.TenderSecification = com;
                    int result = _tenderSecificationManager.Create(model.TenderSecification);

                    if (result == 1)
                    {
                        model.SuccessMessage = "Tender has been created successfully!";

                    }
                    else
                    {
                        model.FailedMessage = "Tender creation failed!";
                    }
                }
                //else
                //{
                //    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSecification.FileNo, model.FlowSerial, FormInformation.TenderDGDP, DateTime.Now);
                //    model.TenderSecification.CreateId = PortalContext.CurrentUser.UserName;

                //}
       

            }
            model.TenderingType = _commonCodeManager.GetCommonCodeByType("TenderingType").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            return View("TenderInfo", model);
        }
        [HttpPost]
        public ActionResult ApproveInfoUpdate(TenderViewModel model)
        {

            if (model.ApproveDocument != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.TenderSecification.ApproveUrl = ImageUpload.ImageUploadFile(model.ApproveDocument, dirFullPath, dirPath);
            }
 
            if (model != null)
            {
                TenderSpecification com = new TenderSpecification();
                com = _tenderSecificationManager.GetTenderSpecification(model.TenderSecification.TenderIdentity);
                if (com != null)
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSecification.FileNo, model.FlowSerial, FormInformation.TenderSpecificationApprove,DateTime.Now);
                    com.IsApproved = model.TenderSecification.IsApproved;
                    if (model.TenderSecification.IsApproved != true)
                    {
                        com.ApproveRemarks = null;
                        com.ApproveUrl =null;
                        com.ApproveDate = null;
                    }
                    else
                    {
      
                        com.ApproveRemarks = model.TenderSecification.ApproveRemarks;
                        com.ApproveUrl = model.TenderSecification.ApproveUrl ?? com.ApproveUrl;
                        com.ApproveDate = model.TenderSecification.ApproveDate;
                        com.Decition = model.TenderSecification.Decition;
                        com.Reference = model.TenderSecification.Reference;
                    }
                    int result = _tenderSecificationManager.Create(com);
                    if (result == 1)
                    {
                        model.SuccessMessage = "Tender Approved successfully!";

                    }
                    else
                    {
                        model.FailedMessage = "Tender Approved failed!";
                    }

                }

            }
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            return View("TenderApproveIndex", model);
        }
      public ActionResult TenderOfferIndex(TenderViewModel model)
      {
          model.TenderOffers = _tenderOfferManager.GetAll();
            return View(model);
      }


      [HttpGet]
      public ActionResult TenderCreate(long? TenderId, TenderViewModel model)
      {

          model.TenderOffer = _tenderOfferManager.GetTenderOffer(TenderId) ?? new TenderOffer();

          return View(model);
      }

      [HttpPost]
      public ActionResult TenderCreate(TenderViewModel model)
      {
          if (model != null)
          {
              TenderOffer com = new TenderOffer();
              com = _tenderOfferManager.GetTenderOffer(model.TenderOffer.Id);
              if (com != null)
              {
                  com.Id = model.TenderOffer.Id;
                  com.ProjectName = model.TenderOffer.ProjectName;
                  com.DespatchNumber = model.TenderOffer.DespatchNumber;
                  com.Subject = model.TenderOffer.Subject;
                  com.MainSubject = model.TenderOffer.MainSubject;
                  com.IssueDate = model.TenderOffer.IssueDate;
                  com.Including = model.TenderOffer.Including;
                  com.SigningAuthority = model.TenderOffer.SigningAuthority;
                  com.LastUpdateId = PortalContext.CurrentUser.UserName;
                  com.LastUpdateDate = DateTime.Now;
                  model.TenderOffer = com;

              }
              else
              {
                  model.TenderOffer.CreateId = PortalContext.CurrentUser.UserName;
                  model.TenderOffer.CreateDate = DateTime.Now;
              }
              int result = _tenderOfferManager.Create(model.TenderOffer);

              if (result == 1)
              {
                  model.SuccessMessage = "Demand has been created successfully!";

              }
              else
              {
                  model.FailedMessage = "Demand creation failed!";
              }

          }

          return View("Create", model);
      }

      public ActionResult AddNewRow(TenderViewModel model)
      {
          model.Key = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
          model.TenderChildDictionary.Add(model.Key, new TenderSpecificationChild());
        
          return PartialView("_newRow", model);
      }

        public ActionResult DeleteTenderChild(int? id)
        {
            _tenderSecificationManager.DeleteTenderChild(id);
            return Json("ss",JsonRequestBehavior.AllowGet);
        }

	}
}