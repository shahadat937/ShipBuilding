using System;
using System.Collections.Generic;

namespace RMS.Model
{
   public partial class Demand
    {
        public Demand()
        {
            this.AFDLetters = new HashSet<AFDLetter>();
            this.CommitteeRaiseLetters = new HashSet<CommitteeRaiseLetter>();
            this.ContractDeptOpinions = new HashSet<ContractDeptOpinion>();
            this.ContractFiles = new HashSet<ContractFile>();
            this.FileUploadForms = new HashSet<FileUploadForm>();
            this.FinanceMinistryLetters = new HashSet<FinanceMinistryLetter>();
            this.FinancialInstallments = new HashSet<FinancialInstallment>();
            this.FlowSetups = new HashSet<FlowSetup>();
            this.ProjectNotes = new HashSet<ProjectNote>();
            this.StaffRequirementDeptOpinions = new HashSet<StaffRequirementDeptOpinion>();
            this.TenderSpecDeptOpinions = new HashSet<TenderSpecDeptOpinion>();
            this.TenderSpecificationChilds = new HashSet<TenderSpecificationChild>();
            this.TenderSpecOpinions = new HashSet<TenderSpecOpinion>();
            this.ContractSigns = new HashSet<ContractSign>();
            this.StaffRequirementOpinions = new HashSet<StaffRequirementOpinion>();
            this.ContractOpinions = new HashSet<ContractOpinion>();
        }

        public long DemandId { get; set; }
        public Nullable<long> OrganigationId { get; set; }
        public Nullable<long> ChildOrganization { get; set; }
        public Nullable<long> ProjectType { get; set; }
        public Nullable<long> ProjectCat { get; set; }
        public Nullable<byte> ControllType { get; set; }
        public string FileNo { get; set; }
        public Nullable<long> Category { get; set; }
        public Nullable<bool> IsAccept { get; set; }
        public Nullable<System.DateTime> AcceptStatusDate { get; set; }
        public string FileUrl { get; set; }
        public string DemandingUrl { get; set; }
        public string EndDate { get; set; }
        public Nullable<bool> IsComplete { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }

        public virtual ICollection<AFDLetter> AFDLetters { get; set; }
        public virtual ICollection<CommitteeRaiseLetter> CommitteeRaiseLetters { get; set; }
        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual ICollection<ContractDeptOpinion> ContractDeptOpinions { get; set; }
        public virtual ICollection<ContractFile> ContractFiles { get; set; }
        public virtual ICollection<ContractSign> ContractSigns { get; set; }
        public virtual ControlType ControlType { get; set; }
        public virtual ICollection<FileUploadForm> FileUploadForms { get; set; }
        public virtual ICollection<FinanceMinistryLetter> FinanceMinistryLetters { get; set; }
        public virtual ICollection<FinancialInstallment> FinancialInstallments { get; set; }
        public virtual ICollection<FlowSetup> FlowSetups { get; set; }
        public virtual ICollection<ProjectNote> ProjectNotes { get; set; }
        public virtual ICollection<StaffRequirementDeptOpinion> StaffRequirementDeptOpinions { get; set; }
        public virtual ICollection<TenderSpecDeptOpinion> TenderSpecDeptOpinions { get; set; }
        public virtual ICollection<TenderSpecificationChild> TenderSpecificationChilds { get; set; }
        public virtual ICollection<TenderSpecOpinion> TenderSpecOpinions { get; set; }
        public virtual ICollection<StaffRequirementOpinion> StaffRequirementOpinions { get; set; }
        public virtual ICollection<ContractOpinion> ContractOpinions { get; set; }
    
    }
}
