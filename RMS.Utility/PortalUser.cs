using System;

namespace RMS.Utility
{
    public class PortalUser
    {
        public long BankCodeIdentity { get; set; }
        public long DistrictCodeIdentity { get; set; }
        public long BranchInfoIdentity { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string BranchCode { get; set; }
        public string BankName { get; set; }
        public string DistrictName { get; set; }
        public string BranchName { get; set; }
        public long CountryCode { get; set; }
        public string CountryName { get; set; }
        public long CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string BankCode { get; set; }
        public string UserFullName { get; set; }
        public decimal TransLimit { get; set; }
        public string SubBranchCode { get; set; }
        public string SubBranchName { get; set; }
        public string QryType { get; set; }
        public string Code { get; set; }
        public bool IsValidate { get; set; }
        public int RoleId { get; set; }
        public string UserRole { get; set; }
        public string NativeBranchCode { get; set; }
        public string LoweredRoleName { get; set; }
        public string MenuTitle { get; set; }
        public bool WinPassword { get; set; }
        public bool IsFirstTime { get; set; }
        public bool IsUserInRole(string role) { return System.Web.Security.Roles.Provider.IsUserInRole(UserName, role); }
    }
}
