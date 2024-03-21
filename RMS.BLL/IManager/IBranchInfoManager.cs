using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IBranchInfoManager
    {
        List<BranchInfo> GetBankList();

        List<BranchInfo> GetDistrictList(string bankCode);
        List<BranchInfo> GetBranchList(string districtCode);

        List<ZoneInfo> ZoneInfoes();

        ZoneInfo FineOneZoneInfo(long zoneInfoIdenity);
        ResponseModel SaveZoneInfo(ZoneInfo zoneInfo);

        ResponseModel DeleteZoneInfo(long zoneInfoIdentity);

        BranchInfo FineOneByBranchInfoIdentity(long branchInfoIdentiy);
        ResponseModel SaveBankInfo(BranchInfo branchInfo);

        ResponseModel DeleteBank(long branchInfoIdentity);

        ResponseModel SaveDistrictInfo(BranchInfo branchInfo);

        ResponseModel DeleteDistrict(long branchInfoIdentity);

        List<BranchInfo> GetBranchListByBankAndDistrict(string bankCode, string districtCode);

        ResponseModel SaveBranchInfo(BranchInfo branchInfo);

        BranchInfo FineOneByBranchCode(string branchCode);


        List<BranchInfo> GetExchangeHouseDivisionList(string bankCode);

        List<BranchInfo> GetSubBranchInfoes(string branchCode);

        ResponseModel SaveSubBranchInfo(BranchInfo branchInfo);

        ResponseModel DeleteBranchInfo(long branchInfoIdentity);

        ResponseModel DeleteSubBranchInfo(long branchInfoIdentity);


        List<BranchInfo> GetFourthLevelList();

        List<BranchInfo> ExchangeHouseList();

        BankInfo GetBankInfo(long respondingBranch);

        List<BankInfo> GetBankListFromBankInfo();

        List<BankInfo> GetDistrictListFromBankInfo(string p);

        List<BranchInfo> GetBranchListByBankCode();

        BranchInfo FindExchangeByLCAccountNo(string accountLC);

        ZoneInfo FineOneZoneInfoByCode(string zoneCode);
    }
}
