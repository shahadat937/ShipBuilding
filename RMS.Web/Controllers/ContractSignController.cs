using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Org.BouncyCastle.Asn1.CryptoPro;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class ContractSignController : Controller
    {
        private readonly IContractSignManager _contractSignManager;
        private readonly IDemandManager _demandManager;
        private readonly IFlowSetupManager _flowSetupManager;
        public ContractSignController(IDemandManager _demandManager, IContractSignManager _contractSignManager, IFlowSetupManager _flowSetupManager)
        {
            this._contractSignManager = _contractSignManager;
            this._demandManager = _demandManager;
            this._flowSetupManager = _flowSetupManager;
        }
        public ActionResult Index(string flowserial, long? proId, ContractSignViewModel model)
        {
            model.ContractSigns = _contractSignManager.GetAllbyId(proId);
            model.ContractSign.Demandid = proId != null ? Convert.ToInt64(proId) : 0;
            model.FlowSerial = flowserial;
            return View(model);
        }

        public ActionResult ContractSignEdit(long? fileId, string flowserial, ContractSignViewModel model)
        {

            model.ContractSign = _contractSignManager.GetById(fileId) ?? new ContractSign();
            model.projectLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();

          
             model.FlowSerial = flowserial;
             model.FileId = fileId;
          
            return View(model);
        }

        public ActionResult Save(ContractSignViewModel model)
        {
            if (model != null)
            {
                ContractSign com = new ContractSign();
                com = _contractSignManager.GetById(model.FileId);
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.ContractSign.FilePath = ImageUpload.ImageUploadFile(model.DemandingUpload, dirFullPath, dirPath);
                if (com != null)
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.FileId, model.FlowSerial, FormInformation.ContractSign, null);

                    com.Demandid = model.ContractSign.Demandid;
                    com.Reference = model.ContractSign.Reference;
                    com.SendName = model.ContractSign.SendName;
                    com.SendDate = model.ContractSign.SendDate;
                    com.FilePath = model.ContractSign.FilePath ?? com.FilePath;
                    com.Remarks = model.ContractSign.Remarks;

                    com.LastUpdateId = PortalContext.CurrentUser.UserName;
                    com.LastUpdateDate = DateTime.Now;
                    model.ContractSign = com;

                }
                else
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.FileId, model.FlowSerial, FormInformation.ContractSign, DateTime.Now);

                    model.ContractSign.CreateId = PortalContext.CurrentUser.UserName;
                    model.ContractSign.CreateDate = DateTime.Now;
                }
                int result = _contractSignManager.Create(model.ContractSign);

                if (result == 1)
                {
                    model.SuccessMessage = "Contract Sign has been created successfully!";

                }
                else
                {
                    model.FailedMessage = "Contract Sign creation failed!";
                }

            }
            model.projectLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            return View("ContractSignEdit", model);
        }
	}
}