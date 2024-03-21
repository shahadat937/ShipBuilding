using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HasCode;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class BranchInfoManager : BaseManager, IBranchInfoManager
    {
        private IBranchInfoRepository _branchInfoRepository;
        private IZoneInfoRepository _zoneInfoRepository;
        private IBankInfoRepository _bankInfoRepository;
        public BranchInfoManager()
        {
            _branchInfoRepository = new BranchInfoRepository();
            _zoneInfoRepository = new ZoneInfoRepository();
            _bankInfoRepository = new BankInfoRepository();
        }

        public List<BranchInfo> GetBankList()
        {
            return _branchInfoRepository.Filter(x => x.BranchLevel == "1").ToList();
        }

        public List<BranchInfo> GetDistrictList(string bankCode)
        {
            return _branchInfoRepository.Filter(x => x.FirstLevel == bankCode && x.BranchLevel == "2").ToList();
        }

        public List<BranchInfo> GetBranchList(string districtCode)
        {
            return _branchInfoRepository.Filter(x => x.SecondLevel == districtCode && x.BranchLevel == "3").ToList();
        }

        public List<ZoneInfo> ZoneInfoes()
        {
            return _zoneInfoRepository.All().ToList();
        }

        public ZoneInfo FineOneZoneInfo(long zoneInfoIdenity)
        {
            return _zoneInfoRepository.FindOne(x => x.ZoneInfoIdentity == zoneInfoIdenity);
        }
        public ResponseModel SaveZoneInfo(ZoneInfo model)
        {
            ZoneInfo oldData = _zoneInfoRepository.FindOne(x => x.ZoneCode == model.ZoneCode);
            if (oldData == null)
            {
                var obj = new ZoneInfo()
                {
                    ZoneCode = model.ZoneCode,
                    ZoneName = model.ZoneName,
                    //BankCode = PortalContext.CurrentUser.BankCode
                };
                ResponseModel.ResponsStatus = _zoneInfoRepository.Save(obj);
                ResponseModel.Message = "Zone information has been saved.";
            }
            else
            {
                oldData.ZoneCode = model.ZoneCode;
                oldData.ZoneName = model.ZoneName;
                //oldData.BankCode = PortalContext.CurrentUser.BankCode;
                ResponseModel.ResponsStatus = _zoneInfoRepository.Edit(oldData);
                ResponseModel.Message = "Zone information has been updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteZoneInfo(long zoneInfoIdentity)
        {
            ZoneInfo oldData = _zoneInfoRepository.FindOne(x => x.ZoneInfoIdentity == zoneInfoIdentity);
            if (oldData != null)
            {
                ResponseModel.ResponsStatus = _zoneInfoRepository.Delete(x => x.ZoneInfoIdentity == zoneInfoIdentity);
                ResponseModel.ResponsStatus = 1;
                ResponseModel.Message = "Data has been deleted.";
            }
            else
            {
                ResponseModel.Message = "Data is not deleted.";
            }
            return ResponseModel;
        }

        public BranchInfo FineOneByBranchInfoIdentity(long branchInfoIdentiy)
        {
            return _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentiy);
        }

        public ResponseModel SaveBankInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.BranchCode);
            if (oldData == null)
            {
                var obj = new BranchInfo()
                {
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    //CountryCode = PortalContext.CurrentUser.CountryCode,
                    ZoneInfoIdentity = 1,
                    BranchLevel = "1",
                    FirstLevel = model.BranchCode,
                    SecondLevel = "",
                    ThirdLevel = "",
                    FourthLevel = "",
                    FifthLevel = "",
                    BranchType = "0",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    //CurrencyCode = PortalContext.CurrentUser.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "Bank information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "Bank information is updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteBank(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                BranchInfo isExist = _branchInfoRepository.FindOne(x => x.FirstLevel == oldData.BranchCode && x.BranchLevel == "2");
                if (isExist == null)
                {
                    ResponseModel.ResponsStatus = _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                    ResponseModel.ResponsStatus = 1;
                    ResponseModel.Message = "Bank information has been deleted.";
                }
                else
                {
                    ResponseModel.Message = "Bank information is not deleted, there are some district are available.";
                }
            }
            else
            {
                ResponseModel.Message = "Bank information is not deleted.";
            }
            return ResponseModel;
        }

        public ResponseModel SaveDistrictInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.SecondLevel);
            if (model.CountryCode != 112587)
            {
                model.BranchType = "9";
            }
            if (oldData == null)
            {
                var obj = new BranchInfo()
                {
                    BranchCode = model.SecondLevel,
                    BranchName = model.BranchName,
                    BBDistrict=model.BBDistrict,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    //CountryCode = PortalContext.CurrentUser.CountryCode,
                    ZoneInfoIdentity = 1,
                    BranchLevel = "2",
                    FirstLevel = model.FirstLevel,
                    SecondLevel = model.SecondLevel,
                    ThirdLevel = "",
                    FourthLevel = "",
                    FifthLevel = "",
                    BranchType = "0",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    //CurrencyCode = PortalContext.CurrentUser.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "District information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                oldData.BBDistrict = model.BBDistrict;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "District information is updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteDistrict(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                BranchInfo isExist = _branchInfoRepository.FindOne(x => x.SecondLevel == oldData.BranchCode && x.BranchLevel == "3");
                if (isExist == null)
                {
                    ResponseModel.ResponsStatus = _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                    ResponseModel.ResponsStatus = 1;
                    ResponseModel.Message = "District information has been deleted.";
                }
                else
                {
                    ResponseModel.Message = "District information is not deleted, there are some branches are available.";
                }
            }
            else
            {
                ResponseModel.Message = "District information is not deleted.";
            }
            return ResponseModel;
        }

        public List<BranchInfo> GetBranchListByBankAndDistrict(string bankCode, string districtCode)
        {
            return _branchInfoRepository.Filter(x => x.FirstLevel == bankCode && x.SecondLevel == districtCode && x.BranchLevel == "3").ToList();
        }

        public ResponseModel SaveBranchInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.BranchCode);
            if (model.CountryCode != 112587)
            {
                model.BranchType = "9";
            }
            if (oldData == null)
            {

                var obj = new BranchInfo()
                {
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    CountryCode = model.CountryCode,
                    ZoneInfoIdentity = model.ZoneInfoIdentity,
                    BranchLevel = "3",
                    FirstLevel = model.FirstLevel,
                    SecondLevel = model.SecondLevel,
                    ThirdLevel = model.BranchCode,
                    FourthLevel = "",
                    FifthLevel = "",
                    BranchType = "0",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    CurrencyCode = model.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance,
                    BBDistrict=model.BBDistrict
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "A New Branch information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                oldData.ContactPerson = model.ContactPerson ?? "";
                oldData.Address = model.Address ?? "";
                oldData.Telephone = model.Telephone ?? "";
                oldData.Cellphone = model.Cellphone ?? "";
                oldData.Email = model.Email ?? "";
                oldData.Fax = model.Fax ?? "";
                oldData.CountryCode = model.CountryCode;
                oldData.ZoneInfoIdentity = model.ZoneInfoIdentity ?? 1;
                oldData.BranchLevel = "3";
                oldData.FirstLevel = model.FirstLevel;
                oldData.SecondLevel = model.SecondLevel;
                oldData.ThirdLevel = model.BranchCode;
                oldData.FourthLevel = "";
                oldData.FifthLevel = "";
                oldData.BranchType = "0";
                oldData.NativeBranchCode = model.NativeBranchCode ?? "";
                oldData.CurrencyCode = model.CurrencyCode;
                oldData.OwnBranchCode = model.OwnBranchCode ?? "";
                oldData.UserId = PortalContext.CurrentUser.UserId;
                oldData.LastUpdate = DateTime.Now;
                oldData.ServerName = model.ServerName ?? "";
                oldData.AccountNoFC = model.AccountNoFC ?? "";
                oldData.AccountNoLC = model.AccountNoLC ?? "";
                oldData.MinimumCoverFund = model.MinimumCoverFund;
                oldData.WorkingTimeFrom = model.WorkingTimeFrom ?? 10;
                oldData.WorkingTimeTo = model.WorkingTimeTo ?? 18;
                oldData.MinimumNRDBalance = model.MinimumNRDBalance;
                oldData.BBDistrict = model.BBDistrict;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "Branch information is updated.";
            }
            return ResponseModel;
        }

        public BranchInfo FineOneByBranchCode(string branchCode)
        {
            return _branchInfoRepository.FindOne(x => x.BranchCode == branchCode);
        }

        public List<BranchInfo> GetExchangeHouseDivisionList(string bankCode)
        {
            return _branchInfoRepository.Filter(x => x.FirstLevel == bankCode && x.BranchLevel == "2" && x.BranchType == "9").ToList();
        }

        public List<BranchInfo> GetSubBranchInfoes(string branchCode)
        {
            return _branchInfoRepository.Filter(x => x.ThirdLevel == branchCode && x.BranchLevel == "4" && x.BranchType == "9").ToList();
        }

        public ResponseModel SaveSubBranchInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.BranchCode);
            if (oldData == null)
            {
                var obj = new BranchInfo()
                {
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    CountryCode = model.CountryCode,
                    ZoneInfoIdentity = model.ZoneInfoIdentity ?? 1,
                    BranchLevel = "4",
                    FirstLevel = model.FirstLevel,
                    SecondLevel = model.SecondLevel,
                    ThirdLevel = model.ThirdLevel,
                    FourthLevel = model.BranchCode,
                    FifthLevel = "",
                    BranchType = "9",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    CurrencyCode = model.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "A New Sub Branch information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                oldData.ContactPerson = model.ContactPerson ?? "";
                oldData.Address = model.Address ?? "";
                oldData.Telephone = model.Telephone ?? "";
                oldData.Cellphone = model.Cellphone ?? "";
                oldData.Email = model.Email ?? "";
                oldData.Fax = model.Fax ?? "";
                oldData.CountryCode = model.CountryCode;
                oldData.ZoneInfoIdentity = model.ZoneInfoIdentity ?? 1;
                oldData.BranchLevel = "4";
                oldData.FirstLevel = model.FirstLevel;
                oldData.SecondLevel = model.SecondLevel;
                oldData.ThirdLevel = model.ThirdLevel;
                oldData.FourthLevel = model.BranchCode;
                oldData.FifthLevel = "";
                oldData.BranchType = "9";
                oldData.NativeBranchCode = model.NativeBranchCode ?? "";
                oldData.CurrencyCode = model.CurrencyCode;
                oldData.OwnBranchCode = model.OwnBranchCode ?? "";
                oldData.UserId = PortalContext.CurrentUser.UserId;
                oldData.LastUpdate = DateTime.Now;
                oldData.ServerName = model.ServerName ?? "";
                oldData.AccountNoFC = model.AccountNoFC ?? "";
                oldData.AccountNoLC = model.AccountNoLC ?? "";
                oldData.MinimumCoverFund = model.MinimumCoverFund;
                oldData.WorkingTimeFrom = model.WorkingTimeFrom ?? 10;
                oldData.WorkingTimeTo = model.WorkingTimeTo ?? 18;
                oldData.MinimumNRDBalance = model.MinimumNRDBalance;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "Sub Branch information is updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteBranchInfo(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                BranchInfo isExist =
                    _branchInfoRepository.FindOne(x => x.ThirdLevel == oldData.BranchCode && x.BranchLevel == "4");
                if (isExist == null)
                {
                    ResponseModel.ResponsStatus =
                        _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                    ResponseModel.ResponsStatus = 1;
                    ResponseModel.Message = "Branch information has been deleted.";
                }
                else
                {
                    ResponseModel.Message =
                        "Branch information is not deleted, becasue of there are some sub branch are available in this branch.";
                }
            }
            else
            {
                ResponseModel.Message = "Invalid branch code.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteSubBranchInfo(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                ResponseModel.ResponsStatus = _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                ResponseModel.ResponsStatus = 1;
                ResponseModel.Message = "Sub Branch information has been deleted.";
            }
            else
            {
                ResponseModel.Message = "Invalid branch code.";
            }
            return ResponseModel;
        }

        public List<BranchInfo> GetFourthLevelList()
        {
            return _branchInfoRepository.Filter(x => x.BranchLevel == "4").ToList();
        }

        public List<BranchInfo> ExchangeHouseList()
        {
            return _branchInfoRepository.Filter(x => x.CountryCode != 112587 && x.BranchLevel == "4").ToList();
        }

       
        public BankInfo GetBankInfo(long respondingBranch)
        {
            return _bankInfoRepository.FindOne(x => x.BranchInfoIdentity == respondingBranch);
        }

        public List<BankInfo> GetBankListFromBankInfo()
        {
            return _bankInfoRepository.Filter(x => x.BranchLevel == "1").ToList();
        }

        public List<BankInfo> GetDistrictListFromBankInfo(string bankCode)
        {
            return
                _bankInfoRepository.Filter(
                    x => x.BankCode == bankCode && x.BranchLevel == "2" && x.CountryCode == 112587).ToList();
        }

        public List<BranchInfo> GetBranchListByBankCode()
        {
            return
                _branchInfoRepository.Filter(
                    x =>
                        x.BranchLevel == "3" &&
                        x.CountryCode == 112587).ToList();
        }

        public BranchInfo FindExchangeByLCAccountNo(string accountLC)
        {
            return _branchInfoRepository.FindOne(x => x.AccountNoLC == accountLC);
        }

        public ZoneInfo FineOneZoneInfoByCode(string zoneCode)
        {
            return _zoneInfoRepository.FindOne(x => x.ZoneCode == zoneCode);
        }

        static double StringCompare(string a, string b)
        {
            a = a.Trim();
            b = b.Trim();

            a = a.Replace("bank", "");
            a = a.Replace("limited", "");
            a = a.Replace("ltd", "");
            a = a.Replace("-", "");
            a = a.Replace(",", "");
            a = a.Replace("branch", "");
            a = a.Replace("br", "");
            a = a.Replace("br.", "");
            a = a.Replace("district", "");
            a = a.Replace("dist", "");
            a = a.Replace(".", "");
            a = a.Replace("the", "");
            a = a.Replace(" ", "");
            a = a.Replace("zone", "");
            a = a.Replace("(", "");
            a = a.Replace(")", "");

            b = b.Replace("bank", "");
            b = b.Replace("limited", "");
            b = b.Replace("ltd", "");
            b = b.Replace("-", "");
            b = b.Replace(",", "");
            b = b.Replace("branch", "");
            b = b.Replace("br", "");
            b = b.Replace("br.", "");
            b = b.Replace("district", "");
            b = b.Replace("dist", "");
            b = b.Replace(".", "");
            b = b.Replace("the", "");
            b = b.Replace(" ", "");
            b = b.Replace("zone", "");
            b = b.Replace("(", "");
            b = b.Replace(")", "");

            a = Regex.Replace(a, @"[0-9\-]", string.Empty);
            b = Regex.Replace(b, @"[0-9\-]", string.Empty);
            if (a == b) //Same string, no iteration needed.
                return 100;
            if ((a.Length == 0) || (b.Length == 0)) //One is empty, second is not
            {
                return 0;
            }
            double maxLen = a.Length > b.Length ? a.Length : b.Length;
            int minLen = a.Length < b.Length ? a.Length : b.Length;
            int sameCharAtIndex = 0;
            for (int i = 0; i < minLen; i++) //Compare char by char
            {
                if (a[i] == b[i])
                {
                    sameCharAtIndex++;
                }
            }
            return sameCharAtIndex / maxLen * 100;
        }

    }
}
