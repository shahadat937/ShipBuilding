using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IContractManager _iContractManager;
        private readonly IContractFieldManager _iContractFieldManager;
        private readonly IContractFileManager _iContractFileManager;
        public ContractsController(IContractManager iContractManager, IContractFieldManager iContractFieldManager,
            IContractFileManager iContractFileManager)
        {
            _iContractManager = iContractManager;
            _iContractFieldManager = iContractFieldManager;
            _iContractFileManager = iContractFileManager;
        }
        public ActionResult Index(ContractViewModel model)
        {
            model.Contracts = _iContractManager.GetAllContracts();
            foreach (var item in model.Contracts)
            {
                item.ContractFileName = _iContractFileManager.GetContractFile(Convert.ToInt64(item.ContractFileId));
                item.FieldName = _iContractFieldManager.GetContractFieldNameById(Convert.ToInt64(item.FieldId));
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(ContractViewModel model)
        {
            string fileName = "ContractAgreement";
            model.Contract.ContractFileId = _iContractFileManager.GetContractFileIdByName(fileName);

            model.ContractFieldSelectedItems = ContractFields();
            return View(model);
        }
        [HttpPost, ActionName("Create")]
        public ActionResult ConfirmedCreate(ContractViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Message = _iContractManager.SaveContract(model.Contract);
            model.Contracts = _iContractManager.GetAllContracts();
            model.ContractFieldSelectedItems = ContractFields();
            foreach (var item in model.Contracts)
            {
                item.ContractFileName = _iContractFileManager.GetContractFile(Convert.ToInt64(item.ContractFileId));
                item.FieldName = _iContractFieldManager.GetContractFieldNameById(Convert.ToInt64(item.FieldId));
            }
            return View("_ContracListPatialView", model);
        }
        [HttpGet]
        public ActionResult Edit(string Id, ContractViewModel model)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            string fileName = "ContractAgreement";
            model.Contract.ContractFileId = _iContractFileManager.GetContractFileIdByName(fileName);

            long contId = Convert.ToInt64(Id);
            model.Contract = _iContractManager.GetContractById(contId);
            model.ContractFieldSelectedItems = ContractFields();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ContractViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Message = _iContractManager.SaveContract(model.Contract);
            model.ContractFieldSelectedItems = ContractFields();
            return View(model);
        }
        public List<SelectionList> ContractFields()
        {
            List<SelectionList> items = new List<SelectionList>();
            List<ContractField> fields = _iContractFieldManager.GetAllContractFields();
            fields.ForEach(x => items.Add(new SelectionList() { Text = x.CommonCode.TypeValue, Value = (long) x.FieldName }));
            return items;
        }
        public List<SelectListItem> ContractFiles()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<ContractFile> files = _iContractFileManager.GetAllContractFiles();
            files.ForEach(x => items.Add(new SelectListItem() { Text = x.FileName, Value = x.ContractFileId.ToString() }));
            return items;
        }
    }
}