//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class BranchInfo
    {
        public BranchInfo()
        {
            this.CommonCodes = new HashSet<CommonCode>();
            this.EventLogs = new HashSet<EventLog>();
            this.Notices = new HashSet<Notice>();
            this.Users = new HashSet<User>();
        }
    
        public long BranchInfoIdentity { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public Nullable<long> CountryCode { get; set; }
        public Nullable<long> ZoneInfoIdentity { get; set; }
        public string BranchLevel { get; set; }
        public string FirstLevel { get; set; }
        public string SecondLevel { get; set; }
        public string ThirdLevel { get; set; }
        public string FourthLevel { get; set; }
        public string FifthLevel { get; set; }
        public string BranchType { get; set; }
        public string NativeBranchCode { get; set; }
        public Nullable<long> CurrencyCode { get; set; }
        public string OwnBranchCode { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public string ServerName { get; set; }
        public string AccountNoFC { get; set; }
        public string AccountNoLC { get; set; }
        public Nullable<decimal> MinimumCoverFund { get; set; }
        public Nullable<byte> WorkingTimeFrom { get; set; }
        public Nullable<byte> WorkingTimeTo { get; set; }
        public Nullable<decimal> MinimumNRDBalance { get; set; }
        public string BBDistrict { get; set; }
    
        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual ICollection<CommonCode> CommonCodes { get; set; }
        public virtual ICollection<EventLog> EventLogs { get; set; }
        public virtual ICollection<Notice> Notices { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
