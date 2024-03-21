using Newtonsoft.Json;
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
    public class ContractFieldsController : Controller
    {
        private readonly IContractFieldManager _iContractFieldManager;
        private readonly ICommonCodeManager _commonCodeManager;
        public ContractFieldsController(IContractFieldManager iContractFieldManager, ICommonCodeManager commonCodeManager)
        {
            _iContractFieldManager = iContractFieldManager;
            _commonCodeManager = commonCodeManager;
        }
        [HttpGet]
        public ActionResult Create(string Id, ContractFieldViewModel model)
        {
            long demandID = Convert.ToInt64(Id);
            model.ContractFieldItem = _commonCodeManager.GetCommonCodeByType("Contract Field").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            List<ContractField> contractFields = _iContractFieldManager.GetAllContractFieldsByDemandId(demandID);
            if (contractFields.Count > 0)
            {
                int i = 0;
                foreach (var item in contractFields)
                {
                    i++;
                    string time = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000000000);
                    time = time + Convert.ToString(i);
                    model.Key = time;
                    model.ContractFieldDictionary.Add(model.Key, item);
                }
            }
            else
            {
                model.ContractFieldDictionary.Add("1", new ContractField());
            }
            model.ContractField.ContractFileId = demandID;           
            return View(model);
        }
        [HttpPost, ActionName("Create")]
        public ActionResult ConfirmedCreate(ContractFieldViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            List<ContractField> contractFields = model.ContractFieldDictionary.Select(x => x.Value).ToList();
            foreach (var contractField in contractFields)
            {
                contractField.ContractFileId = model.ContractField.ContractFileId; ;
                model.ContractFields.Add(contractField);
            }
            _iContractFieldManager.DeleteContractFieldByDemandId(model.ContractField.ContractFileId);
            model.Message = _iContractFieldManager.SaveContractFields(model.ContractFields);
            model.ContractFieldItem = _commonCodeManager.GetCommonCodeByType("Contract Field").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View(model);
        }
        public ActionResult AddNewRow(ContractFieldViewModel model)
        {
            model.Key = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            model.ContractFieldDictionary.Add(model.Key, new ContractField());
            model.ContractFieldItem = _commonCodeManager.GetCommonCodeByType("Contract Field").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return PartialView("_newRow", model);
        }
    }
}