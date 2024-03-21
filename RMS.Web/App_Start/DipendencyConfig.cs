using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;

namespace RMS.Web.App_Start
{
    public class DipendencyConfig
    {
        private static IContainer _container;

        public static void SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerRequest();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerRequest();
            builder.RegisterType<UserURLRightManager>().As<IUserURLRightManager>().InstancePerRequest();
            builder.RegisterType<CommonCodeManager>().As<ICommonCodeManager>().InstancePerRequest();
            builder.RegisterType<BranchInfoManager>().As<IBranchInfoManager>().InstancePerRequest();
            builder.RegisterType<NoticeManager>().As<INoticeManager>().InstancePerRequest();
            builder.RegisterType<ReportingManager>().As<IReportingManager>().InstancePerRequest();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepositoty>().InstancePerRequest();
            builder.RegisterType<DepartmentManager>().As<IDepartmentManager>().InstancePerRequest();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>().InstancePerRequest();
            builder.RegisterType<MemberManager>().As<IMemberManager>().InstancePerRequest();
            builder.RegisterType<CommiteeManager>().As<ICommiteeManager>().InstancePerRequest();
            builder.RegisterType<CommiteeRepository>().As<ICommiteeRepository>().InstancePerRequest();

            builder.RegisterType<FlowSetupManager>().As<IFlowSetupManager>().InstancePerRequest();
            builder.RegisterType<FlowSetupRepository>().As<IFlowSetupRepository>().InstancePerRequest();

            builder.RegisterType<FlowListManager>().As<IFlowListManager>().InstancePerRequest();
            builder.RegisterType<FlowListRepository>().As<IFlowListRepository>().InstancePerRequest();

            builder.RegisterType<StaffRequirementRepository>().As<IStaffRequirementRepository>().InstancePerRequest();
            builder.RegisterType<StaffRequirementManager>().As<IStaffRequirementManager>().InstancePerRequest();

            builder.RegisterType<FileMovementManager>().As<IFileMovementManager>().InstancePerRequest();
            builder.RegisterType<FileMovementRepository>().As<IFileMovementRepository>().InstancePerRequest();

            builder.RegisterType<ProjectMovementManager>().As<IProjectMovementManager>().InstancePerRequest();
            builder.RegisterType<ProjectMovementRepository>().As<IProjectMovementRepository>().InstancePerRequest();

            builder.RegisterType<StatusRepository>().As<IStatusRepository>().InstancePerRequest();
            builder.RegisterType<StatusManager>().As<IStatusManager>().InstancePerRequest();

            builder.RegisterType<ControlTypeRepository>().As<IControlTypeRepository>().InstancePerRequest();
            builder.RegisterType<ControlTypeManager>().As<IControlTypeManager>().InstancePerRequest();

            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerRequest();
            builder.RegisterType<ProjectManager>().As<IProjectManager>().InstancePerRequest();

            builder.RegisterType<ProjectTypeRepository>().As<IProjectTypeRepository>().InstancePerRequest();
            builder.RegisterType<ProjectTypeManager>().As<IProjectTypeManager>().InstancePerRequest();

            builder.RegisterType<FileRepository>().As<IFileRepository>().InstancePerRequest();
            builder.RegisterType<FileManager>().As<IFileManager>().InstancePerRequest();

            builder.RegisterType<ProjectFinalStatuRepository>().As<IProjectFinalStatuRepository>().InstancePerRequest();
            builder.RegisterType<ProjectFinalStatuManager>().As<IProjectFinalStatuManager>().InstancePerRequest();

            builder.RegisterType<DemandRepository>().As<IDemandRepository>().InstancePerRequest();
            builder.RegisterType<DemandManager>().As<IDemandManager>().InstancePerRequest();


            builder.RegisterType<TenderOfferManager>().As<ITenderOfferManager>().InstancePerRequest();
            builder.RegisterType<TenderOfferRepository>().As<ITenderOfferRepository>().InstancePerRequest();


            builder.RegisterType<TenderSecificationManager>().As<ITenderSecificationManager>().InstancePerRequest();
            builder.RegisterType<TenderSecificationRepository>().As<ITenderSecificationRepository>().InstancePerRequest();

            builder.RegisterType<AgendumManager>().As<IAgendumManager>().InstancePerRequest();
            builder.RegisterType<AgendumRepository>().As<IAgendumRepository>().InstancePerRequest();

            builder.RegisterType<PaymentScheduleManager>().As<IPaymentScheduleManager>().InstancePerRequest();
            builder.RegisterType<PaymentScheduleRepository>().As<IPaymentScheduleRepository>().InstancePerRequest();

            builder.RegisterType<FinanceMinistryLetterManager>().As<IFinanceMinistryLetterManager>().InstancePerRequest();
            builder.RegisterType<FinanceMinistryLetterRepository>().As<IFinanceMinistryLetterRepository>().InstancePerRequest();

            builder.RegisterType<CommitteNameListManager>().As<ICommitteNameListManager>().InstancePerRequest();
            builder.RegisterType<CommitteNameListRepository>().As<ICommitteNameListRepository>().InstancePerRequest();

            builder.RegisterType<TenderSpecOpinionManager>().As<ITenderSpecOpinionManager>().InstancePerRequest();
            builder.RegisterType<TenderSpecOpinionRepository>().As<ITenderSpecOpinionRepository>().InstancePerRequest();

            builder.RegisterType<TenderSpecDeptOpinionManager>().As<ITenderSpecDeptOpinionManager>().InstancePerRequest();
            builder.RegisterType<TenderSpecDeptOpinionRepository>().As<ITenderSpecDeptOpinionRepository>().InstancePerRequest();


            builder.RegisterType<OrganizationManager>().As<IOrganizationManager>().InstancePerRequest();
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerRequest();

            builder.RegisterType<CommitteeRaiseLetterManager>().As<ICommitteeRaiseLetterManager>().InstancePerRequest();
            builder.RegisterType<CommitteeRaiseLetterRepository>().As<ICommitteeRaiseLetterRepository>().InstancePerRequest();

            builder.RegisterType<FormInfoManager>().As<IFormInfoManager>().InstancePerRequest();
            builder.RegisterType<FormInfoRepository>().As<IFormInfoRepository>().InstancePerRequest();

            builder.RegisterType<NSPCMeetingWorkManager>().As<INSPCMeetingWorkManager>().InstancePerRequest();
            builder.RegisterType<NSPCMeetingWorkRepository>().As<INSPCMeetingWorkRepository>().InstancePerRequest();

            builder.RegisterType<ContractFileManager>().As<IContractFileManager>().InstancePerRequest();
            builder.RegisterType<ContractFileRepository>().As<IContractFileRepository>().InstancePerRequest();

            builder.RegisterType<ContractManager>().As<IContractManager>().InstancePerRequest();
            //builder.RegisterType<ContractRepository>().As<IContractRepository>().InstancePerRequest();

            builder.RegisterType<ContractFieldManager>().As<IContractFieldManager>().InstancePerRequest();
            builder.RegisterType<ContractFieldRepository>().As<IContractFieldRepository>().InstancePerRequest();

            builder.RegisterType<ContractSignManager>().As<IContractSignManager>().InstancePerRequest();
            builder.RegisterType<ContractSignRepository>().As<IContractSignRepository>().InstancePerRequest();

            builder.RegisterType<ContractOpinionManager>().As<IContractOpinionManager>().InstancePerRequest();
            builder.RegisterType<ContractOpinionRepository>().As<IContractOpinionRepository>().InstancePerRequest();

            builder.RegisterType<ContractDeptOpinionManager>().As<IContractDeptOpinionManager>().InstancePerRequest();
            builder.RegisterType<ContractDeptOpinionRepository>().As<IContractDeptOpinionRepository>().InstancePerRequest();


            builder.RegisterType<StaffRequirementOpinionManager>().As<IStaffRequirementOpinionManager>().InstancePerRequest();
            builder.RegisterType<StaffRequirementOpinionRepository>().As<IStaffRequirementOpinionRepository>().InstancePerRequest();

            builder.RegisterType<StaffRequirementDeptOpinionManager>().As<IStaffRequirementDeptOpinionManager>().InstancePerRequest();
            builder.RegisterType<StaffRequirementDeptOpinionRepository>().As<IStaffRequirementDeptOpinionRepository>().InstancePerRequest();
            builder.RegisterType<FinanceMinistryLetterManager>().As<IFinanceMinistryLetterManager>().InstancePerRequest();
            builder.RegisterType<FinanceMinistryLetterRepository>().As<IFinanceMinistryLetterRepository>().InstancePerRequest();

            builder.RegisterType<AFDLetterManager>().As<IAFDLetterManager>().InstancePerRequest();
            builder.RegisterType<AFDLetterRepository>().As<IAFDLetterRepository>().InstancePerRequest();
            builder.RegisterType<YearCalenderRepository>().As<IYearCalenderRepository>().InstancePerRequest();

            builder.RegisterType<ProjectInstallmentManager>().As<IProjectInstallmentManager>().InstancePerRequest();
            builder.RegisterType<ProjectInstallmentRepository>().As<IProjectInstallmentRepository>().InstancePerRequest();


            builder.RegisterType<FileUploadFormManager>().As<IFileUploadFormManager>().InstancePerRequest();
            builder.RegisterType<FileUploadFormRepository>().As<IFileUploadFormRepository>().InstancePerRequest();

            builder.RegisterType<AFDLetterApproveManager>().As<IAFDLetterApproveManager>().InstancePerRequest();
            builder.RegisterType<AFDLetterApproveRepository>().As<IAFDLetterApproveRepository>().InstancePerRequest();

            builder.RegisterType<FileBackUpManager>().As<IFileBackUpManager>().InstancePerRequest();
            builder.RegisterType<FileBackUpRepository>().As<IFileBackUpRepository>().InstancePerRequest();

            builder.RegisterType<FinancialYearManager>().As<IFinancialYearManager>().InstancePerRequest();
            builder.RegisterType<FinancialYearRepository>().As<IFinancialYearRepository>().InstancePerRequest();

            builder.RegisterType<FinancialInstallmentManager>().As<IFinancialInstallmentManager>().InstancePerRequest();
            builder.RegisterType<FinancialInstallmentRepository>().As<IFinancialInstallmentRepository>().InstancePerRequest();


            builder.RegisterType<FieldValueBackManager>().As<IFieldValueBackManager>().InstancePerRequest();
            builder.RegisterType<FieldValueBackRepository>().As<IFieldValueBackRepository>().InstancePerRequest();

            builder.RegisterType<ProjectNoteManager>().As<IProjectNoteManager>().InstancePerRequest();
            builder.RegisterType<ProjectNoteRepository>().As<IProjectNoteRepository>().InstancePerRequest();

            builder.RegisterType<BGPGRepository>().As<IBGPGRepository>().InstancePerRequest();
            builder.RegisterFilterProvider();
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private static T Resolve<T>()
        {
            if (_container == null)
                SetupContainer();

            return _container.Resolve<T>();
        }
    }
}