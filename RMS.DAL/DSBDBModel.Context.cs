﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using RMS.Model;
    
    public partial class DSBDBEntities : DbContext
    {
        public DSBDBEntities()
            : base("name=DSBDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BranchInfo> BranchInfoes { get; set; }
        public virtual DbSet<EventLog> EventLogs { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<Reporting> Reportings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserURLRight> UserURLRights { get; set; }
        public virtual DbSet<ZoneInfo> ZoneInfoes { get; set; }
        public virtual DbSet<BankInfo> BankInfoes { get; set; }
        public virtual DbSet<UserBankInfo> UserBankInfoes { get; set; }
        public virtual DbSet<DepartmentWiseMileStone> DepartmentWiseMileStones { get; set; }
        public virtual DbSet<DocumentInfo> DocumentInfoes { get; set; }
        public virtual DbSet<MileStone> MileStones { get; set; }
        public virtual DbSet<Notice> Notices { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectFinalStatu> ProjectFinalStatus { get; set; }
        public virtual DbSet<ProjectMovement> ProjectMovements { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<ProjectFileMovement> ProjectFileMovements { get; set; }
        public virtual DbSet<ProjectStatu> ProjectStatus { get; set; }
        public virtual DbSet<TenderOffer> TenderOffers { get; set; }
        public virtual DbSet<PaymentSchedule> PaymentSchedules { get; set; }
        public virtual DbSet<WithdrawalLetterInfo> WithdrawalLetterInfoes { get; set; }
        public virtual DbSet<Agendum> Agenda { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<FileMovement> FileMovements { get; set; }
        public virtual DbSet<FlowList> FlowLists { get; set; }
        public virtual DbSet<NSPCMeetingWork> NSPCMeetingWorks { get; set; }
        public virtual DbSet<YearCalender> YearCalenders { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<FlowSetup> FlowSetups { get; set; }
        public virtual DbSet<ContractFile> ContractFiles { get; set; }
        public virtual DbSet<ControlType> ControlTypes { get; set; }
        public virtual DbSet<StaffRequirement> StaffRequirements { get; set; }
        public virtual DbSet<ContractField> ContractFields { get; set; }
        public virtual DbSet<FileBackUp> FileBackUps { get; set; }
        public virtual DbSet<FormInfo> FormInfoes { get; set; }
        public virtual DbSet<AFDLetter> AFDLetters { get; set; }
        public virtual DbSet<CommitteNameList> CommitteNameLists { get; set; }
        public virtual DbSet<CommitteInfo> CommitteInfoes { get; set; }
        public virtual DbSet<ProjectInstallment> ProjectInstallments { get; set; }
        public virtual DbSet<FinancialYear> FinancialYears { get; set; }
        public virtual DbSet<BGPG> BGPGs { get; set; }
        public virtual DbSet<FinancialInstallment> FinancialInstallments { get; set; }
        public virtual DbSet<ContractDeptOpinion> ContractDeptOpinions { get; set; }
        public virtual DbSet<StaffRequirementDeptOpinion> StaffRequirementDeptOpinions { get; set; }
        public virtual DbSet<TenderSpecDeptOpinion> TenderSpecDeptOpinions { get; set; }
        public virtual DbSet<FieldValueBack> FieldValueBacks { get; set; }
        public virtual DbSet<CommonCode> CommonCodes { get; set; }
        public virtual DbSet<TenderSpecificationChild> TenderSpecificationChilds { get; set; }
        public virtual DbSet<CommitteeRaiseLetter> CommitteeRaiseLetters { get; set; }
        public virtual DbSet<FinanceMinistryLetter> FinanceMinistryLetters { get; set; }
        public virtual DbSet<FileStore> FileStores { get; set; }
        public virtual DbSet<FileUploadForm> FileUploadForms { get; set; }
        public virtual DbSet<AFDLetterApprove> AFDLetterApproves { get; set; }
        public virtual DbSet<ProjectNote> ProjectNotes { get; set; }
        public virtual DbSet<TenderSpecOpinion> TenderSpecOpinions { get; set; }
        public virtual DbSet<ContractSign> ContractSigns { get; set; }
        public virtual DbSet<TenderSpecification> TenderSpecifications { get; set; }
        public virtual DbSet<StaffRequirementOpinion> StaffRequirementOpinions { get; set; }
        public virtual DbSet<ContractOpinion> ContractOpinions { get; set; }
        public virtual DbSet<Demand> Demands { get; set; }
    
        public virtual ObjectResult<spProjectTaskStatus> spProjectTaskStatus(string projectId)
        {
            var projectIdParameter = projectId != null ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spProjectTaskStatus>("spProjectTaskStatus", projectIdParameter);
        }
    
        public virtual ObjectResult<FileListByTree_Result> FileListByTree()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FileListByTree_Result>("FileListByTree");
        }
    
        public virtual ObjectResult<spProjectOpinion_Result> spProjectOpinion(string projectId)
        {
            var projectIdParameter = projectId != null ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spProjectOpinion_Result>("spProjectOpinion", projectIdParameter);
        }
    }
}
