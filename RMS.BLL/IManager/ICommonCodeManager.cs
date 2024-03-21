using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
     public interface ICommonCodeManager
    {
        ResponseModel SaveCommonCode(CommonCode model);
        List<CommonCode> GetValidPhotoId();
        List<CommonCode> GetCommonCodeTeeView( string Type);
        CommonCode FindOne(long code);
        List<CommonCode> GetCommonCode();
        List<CommonCode> GetCommonCodeByType(string Type);
        List<CommonCode> GetNationalities(string branchCode);
        List<CommonCode> GetCommonType();

        List<CommonCode> GetPhotoIdes(string branchCode);

        List<CommonCode> GetOccupationList(string branchCode);

        List<CommonCode> GetIssuePlaceList(string branchCode);
        List<CommonCode> GetSecurityQustions();
        List<CommonCode> GetOtherBankPaymentPermissionList(string bankCode);
        List<CommonCode> GetRemittanceHeader();
        List<CommonCode> GetConditionList();

        List<CommonCode> GetCurrencyList();

        List<CommonCode> GetTransactionModeList();

        List<CommonCode> GetTransactionNatureList();

        List<CommonCode> GetPaymentModeList();

        List<CommonCode> GetVoucherTypeList();
        string GetCommonCodeName(long? language);
        int DeleteType(string code, string type);
        List<CommonCode> GetTypeBankWise(string accountcategory);

         string GetCommonCodeTypeValueById(long commonCodeTypeValueId);
    }
}
