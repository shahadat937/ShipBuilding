using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class BranchInfoViewModel: AppCommonInfo
    {
        public List<CommonCode> BBDistricts { get; set; } 
        public List<CommonCode> Hours { get; set; } 
        public string BankCode { get; set; }
        public string DistrictCode { get; set; }
        public string BranchCode { get; set; }
        public List<BranchInfo> BankList { get; set; }
        public List<BranchInfo> DistrictList { get; set; } 
        public BranchInfo BranchInfo { get; set; }
        public List<BranchInfo> BranchInfos { get; set; }
        public List<BranchInfo> SubBranchInfos { get; set; } 
        public List<CommonCode> CurrencyList { get; set; }
        public List<CommonCode> CountryList { get; set; }
        public ZoneInfo ZoneInfo { get; set; }
        public List<ZoneInfo> ZoneInfos{ get; set; }
        public BranchInfoViewModel()
        {
            BBDistricts = new List<CommonCode>();
            SubBranchInfos = new List<BranchInfo>();
            Hours = new List<CommonCode>();
            BankList = new List<BranchInfo>();
            DistrictList = new List<BranchInfo>();
            ZoneInfo = new ZoneInfo();
            ZoneInfos = new List<ZoneInfo>();
            BranchInfo = new BranchInfo();
            BranchInfos= new List<BranchInfo>();
            CurrencyList = new List<CommonCode>();
            CountryList = new List<CommonCode>();
        }
        public IEnumerable<SelectListItem> ZoneSelectListItems
        {
            get { return new SelectList(ZoneInfos, "ZoneInfoIdentity", "ZoneName"); }
        }
        public IEnumerable<SelectListItem> BranchInfoesSelectListItems
        {
            get { return new SelectList(BranchInfos, "BranchCode", "BranchName"); }
        }
        public IEnumerable<SelectListItem> CountrySelectListItems
        {
            get { return new SelectList(CountryList, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> BbDistrictSelectedListItems
        {
            get { return new SelectList(BBDistricts, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> CurrencySelectListItems
        {
            get { return new SelectList(CurrencyList, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> HourseSelectListItems
        {
            get { return new SelectList(Hours, "Code", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> BankSelectListItems
        {
            get { return new SelectList(BankList, "BranchCode", "BranchName"); }
        }
        public IEnumerable<SelectListItem> DistrictSelectListItems
        {
            get { return new SelectList(DistrictList, "BranchCode", "BranchName"); }
        }
    }
}