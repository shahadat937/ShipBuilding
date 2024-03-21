using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class CommonCodeManager : BaseManager, ICommonCodeManager
    {


        private ICommonCodeRepository _commonCodeRepository;

        public CommonCodeManager()
        {

            _commonCodeRepository = new CommonCodeRepository();
        }

        public List<CommonCode> GetValidPhotoId()
        {
            return _commonCodeRepository.Filter(x => x.Type == "OtherIDType" && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }
        public List<CommonCode> GetCommonCodeTeeView( string Type)
        {
            var firstLevel = new List<CommonCode>();
            var secondLevel = new List<CommonCode>();

            var commonCodeTrees = _commonCodeRepository.Filter(x =>x.Type == Type);
            var commonCodeTree1 = _commonCodeRepository.Filter(x =>x.Type == Type);
            foreach (CommonCode commonCodeTree in commonCodeTrees)
            {
                firstLevel.Add(commonCodeTree);
                //secondLevel.Add(commonCodeTree);
            }
            //foreach (CommonCode commonCode in firstLevel)
            //{
            //    commonCode.CommonCodeTrees = GetFirstChaild(commonCode.Type, secondLevel).ToList();
            //}

            return firstLevel;
        }
        //private IEnumerable<CommonCode> GetFirstChaild(string Type, List<CommonCode> secondLevel)
        //{
        //    secondLevel = secondLevel.Where(x => x.Type == Type).ToList();            
        //    return secondLevel;
        //}

        public List<CommonCode> GetCommonType()
        {
            return _commonCodeRepository.All().ToList();
        }
        public CommonCode FindOne(long code)
        {
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == code );
        }
        public List<CommonCode> GetCommonCode()
        {
            return _commonCodeRepository.All().ToList();
        }
        public List<CommonCode> GetCommonCodeByType(string Type)
        {
            return _commonCodeRepository.Filter(x => x.Type == Type).ToList();
        }
        public ResponseModel SaveCommonCode(CommonCode model)
        {
            var rec = _commonCodeRepository.Filter(x => x.Code == model.Code && x.Type == model.Type && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
            if (rec.Count <= 0)
            {
                var CCode = new CommonCode
                {
                    BankCode = PortalContext.CurrentUser.BankCode,
                    Code = model.Code.Trim(),
                    Type = model.Type,
                    TypeValue = model.TypeValue,
                    DisplayCode = model.DisplayCode,
                    Status = model.Status
                };

                ResponseModel.ResponsStatus = _commonCodeRepository.Save(CCode);
                ResponseModel.Message = "Common Code Saved successfully";
            }
            else
            {
                var recInfo = rec.First();
                //recInfo.BankCode = PortalContext.CurrentUser.BankCode;
                //recInfo.Code = model.Code;
                recInfo.Type = model.Type;
                recInfo.TypeValue = model.TypeValue;
                recInfo.DisplayCode = model.DisplayCode;
                recInfo.Status = model.Status;
                ResponseModel.ResponsStatus = _commonCodeRepository.Edit(recInfo);
                ResponseModel.Message = "Common Code Updated successfully";

            }
            return ResponseModel;

        }
        public List<CommonCode> GetNationalities(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode && x.Type == "Nationality").ToList();
        }

        public List<CommonCode> GetPhotoIdes(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode && x.Type == "PhotoIdType").ToList();
        }

        public List<CommonCode> GetOccupationList(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode && x.Type == "Occupation").ToList();
        }

        public List<CommonCode> GetIssuePlaceList(string branchCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == branchCode).ToList();
        }

        public List<CommonCode> GetSecurityQustions()
        {
            return
                _commonCodeRepository.Filter(x => x.Type == CommonCodeType.SecurityQuestion.ToString()).ToList();
        }

        public List<CommonCode> GetOtherBankPaymentPermissionList(string bankCode)
        {
            return _commonCodeRepository.Filter(x => x.BankCode == bankCode && x.Type == "OtherBankPayment").ToList();
        }
        public List<CommonCode> GetRemittanceHeader()
        {
            List<CommonCode> remittanceInfoHead = _commonCodeRepository.Filter(x => x.Type == "Inquery" && x.Status == true).ToList();

            return remittanceInfoHead;
        }
        public List<CommonCode> GetConditionList()
        {
            List<CommonCode> condition = _commonCodeRepository.Filter(x => x.Type == "EqualorBetween" && x.Status == true).ToList();

            return condition;
        }

        public List<CommonCode> GetCurrencyList()
        {
            return _commonCodeRepository.Filter(x => x.Type == "Currency" && x.Status == true && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }

        public List<CommonCode> GetTransactionModeList()
        {
            return _commonCodeRepository.Filter(x => x.Type == "Transaction Mode" && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }

        public List<CommonCode> GetTransactionNatureList()
        {
            return _commonCodeRepository.Filter(x => x.Type == "TransactionNature" && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }

        public List<CommonCode> GetPaymentModeList()
        {
            return
                _commonCodeRepository.Filter(
                    x => x.Type == "PaymentMode" && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }

        public List<CommonCode> GetVoucherTypeList()
        {
            return
                _commonCodeRepository.Filter(
                    x => x.Type == "VoucherType" && x.BankCode == PortalContext.CurrentUser.BankCode && x.Status).OrderBy(x => x.TypeValue).ToList();
        }

        public string GetCommonCodeName(long? language)
        {
            var type = "";
            var val = _commonCodeRepository.FindOne(x => x.CommonCodeID == language);
            if (val != null)
            {
                type = val.TypeValue;
            }
            return type;
        }

        public int DeleteType(string code, string type)
        {
            return _commonCodeRepository.Delete(x => x.Code == code && x.Type == type && x.BankCode == PortalContext.CurrentUser.BankCode);

        }

        public List<CommonCode> GetTypeBankWise(string accountcategory)
        {
            return _commonCodeRepository.Filter(x => x.Type == accountcategory && x.BankCode == PortalContext.CurrentUser.BankCode).ToList();
        }

        public string GetCommonCodeTypeValueById(long commonCodeTypeValueId)
        {
    
            return _commonCodeRepository.FindOne(x => x.CommonCodeID == commonCodeTypeValueId).TypeValue;
      
        }
    }
}
