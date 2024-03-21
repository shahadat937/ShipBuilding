using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System.IO;

namespace RMS.BLL.Manager
{
    public class FormInfoManager : BaseManager, IFormInfoManager
    {
        private readonly IFormInfoRepository _iFormInfoRepository;
        private readonly IDemandRepository _iDemandRepository;
        private readonly ITenderSpecOpinionRepository _iTenderSpecOpinionRepository;
        private readonly IStaffRequirementRepository _iStaffRequirementRepository;
        private readonly ICommiteeRepository _iCommiteeRepository;
        private readonly ICommitteNameListRepository _iCommitteNameListRepository;
        private readonly IContractFileRepository _iContractFileRepository;
        private readonly IContractOpinionRepository _iContractOpinionRepository;
        private readonly IContractDeptOpinionRepository _iContractDeptOpinionRepository;
        private readonly ITenderSpecDeptOpinionRepository _iTenderSpecDeptOpinionRepository;
        private readonly IStaffRequirementOpinionRepository _iStaffRequirementOpinionRepository;
        private readonly IStaffRequirementDeptOpinionRepository _iStaffRequirementDeptOpinionRepository;
        private readonly ITenderSecificationRepository _iTenderSecificationRepository;
        private readonly IAgendumRepository _iAgendumRepository;
        private readonly ICommitteeRaiseLetterRepository _iCommitteeRaiseLetterRepository;
        private readonly IFinanceMinistryLetterRepository _iFinanceMinistryLetterRepository;
        public FormInfoManager(IFormInfoRepository iFormInfoRepository, IDemandRepository iDemandRepository,
            ITenderSpecOpinionRepository iTenderSpecOpinionRepository, IStaffRequirementRepository iStaffRequirementRepository,
            ICommiteeRepository iCommiteeRepository, ICommitteNameListRepository iCommitteNameListRepository,
            IContractFileRepository iContractFileRepository, IContractOpinionRepository iContractOpinionRepository,
            IContractDeptOpinionRepository iContractDeptOpinionRepository, ITenderSpecDeptOpinionRepository iTenderSpecDeptOpinionRepository,
            IStaffRequirementOpinionRepository iStaffRequirementOpinionRepository,
            IStaffRequirementDeptOpinionRepository iStaffRequirementDeptOpinionRepository,
            ITenderSecificationRepository iTenderSecificationRepository,
            IAgendumRepository iAgendumRepository, ICommitteeRaiseLetterRepository iCommitteeRaiseLetterRepository,
            IFinanceMinistryLetterRepository iFinanceMinistryLetterRepository)
        {
            _iFormInfoRepository = iFormInfoRepository;
            _iDemandRepository = iDemandRepository;
            _iTenderSpecOpinionRepository = iTenderSpecOpinionRepository;
            _iStaffRequirementRepository = iStaffRequirementRepository;
            _iCommiteeRepository = iCommiteeRepository;
            _iCommitteNameListRepository = iCommitteNameListRepository;
            _iContractFileRepository = iContractFileRepository;
            _iContractOpinionRepository = iContractOpinionRepository;
            _iContractDeptOpinionRepository = iContractDeptOpinionRepository;
            _iTenderSpecDeptOpinionRepository = iTenderSpecDeptOpinionRepository;
            _iStaffRequirementOpinionRepository = iStaffRequirementOpinionRepository;
            _iStaffRequirementDeptOpinionRepository = iStaffRequirementDeptOpinionRepository;
            _iTenderSecificationRepository = iTenderSecificationRepository;
            _iAgendumRepository = iAgendumRepository;
            _iCommitteeRaiseLetterRepository = iCommitteeRaiseLetterRepository;
            _iFinanceMinistryLetterRepository = iFinanceMinistryLetterRepository;
        }

        public FormInfo FindbyId(long id)
        {
            return _iFormInfoRepository.FindOne(x => x.FormId == id);
        }

        public List<FormInfo> GetAll()
        {
            return _iFormInfoRepository.All().OrderBy(x => x.FormName).ToList() ;
        }
        public List<FormInfo> GetSpecificFormInfoById(string id, string fileName)
        {
            List<FormInfo> formInfoes = new List<FormInfo>();
            List<FormInfo> formInfoList = _iFormInfoRepository.All().ToList();
            if (FormInformation.TenderSpecificationOpinion == id)
            {
                List<TenderSpecOpinion> tenderSpecOpinions = _iTenderSpecOpinionRepository.Filter(x => x.TenderUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (tenderSpecOpinions.Count > 0)
                {
                    foreach (var item in tenderSpecOpinions)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.TenderUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.TenderUrl;
                        formInfoes.Add(formInfo);
                    }
                    return formInfoes;
                }
                else
                {
                    List<TenderSpecOpinion> tenderSpecOps = _iTenderSpecOpinionRepository.All().ToList();
                    foreach (var item in tenderSpecOps)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.TenderUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.TenderUrl;
                        formInfoes.Add(formInfo);
                    }
                    return formInfoes;
                }
            }
            else if (FormInformation.TenderSpecification == id || FormInformation.TenderSpecificationApprove == id)
            {

                List<TenderSpecification> tenderSpecifications = _iTenderSecificationRepository.Filter(x => x.FileUrl.ToLower().Contains(fileName)).ToList();
                if (tenderSpecifications.Count > 0)
                {
                    foreach (var item in tenderSpecifications)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<TenderSpecification> tenderSpes = _iTenderSecificationRepository.All().ToList();
                    foreach (var item in tenderSpes)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.Agenda == id)
            {
                List<Agendum> agendas = _iAgendumRepository.Filter(x => x.FileUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (agendas.Count > 0)
                {
                    foreach (var item in agendas)
                    {
                        FormInfo formInfo = new FormInfo();
                        Int64 projectId = Convert.ToInt64(item.FileNo);
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == projectId).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<Agendum> agendaList = _iAgendumRepository.All().ToList();
                    foreach (var item in agendaList)
                    {
                        FormInfo formInfo = new FormInfo();
                        Int64 projectId = Convert.ToInt64(item.FileNo);
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == projectId).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.StaffRequirement == id || FormInformation.StaffRequirementApprove == id)
            {
                List<StaffRequirement> staffRequirements = _iStaffRequirementRepository.Filter(x => x.FileUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (staffRequirements.Count > 0)
                {
                    foreach (var item in staffRequirements)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<StaffRequirement> staffRequirementList = _iStaffRequirementRepository.All().ToList();
                    foreach (var item in staffRequirementList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.Committe == id)
            {
                List<CommitteInfo> committeInfoes = _iCommiteeRepository.All().ToList();
                foreach (var item in committeInfoes)
                {
                    FormInfo formInfo = new FormInfo();
                    formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                    formInfo.FileName = _iCommitteNameListRepository.FindOne(x => x.Id == item.CommitteName).CommitteName;
                    formInfoes.Add(formInfo);
                }
                return formInfoes;
            }
            else if (FormInformation.ContractFile == id || FormInformation.ContractFileApprove == id)
            {
                List<ContractFile> contractFiles = _iContractFileRepository.Filter(x => x.FileUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (contractFiles.Count > 0)
                {
                    foreach (var item in contractFiles)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.DemandId).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<ContractFile> contractFileList = _iContractFileRepository.All().ToList();
                    foreach (var item in contractFileList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.DemandId).FileNo;
                        string filePath = item.FileUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.FileUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.ContractOpinion == id)
            {
                List<ContractOpinion> contractOpinions = _iContractOpinionRepository.Filter(x => x.TenderUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (contractOpinions.Count > 0)
                {
                    foreach (var item in contractOpinions)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.TenderUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.TenderUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<ContractOpinion> contractOpinionList = _iContractOpinionRepository.All().ToList();
                    foreach (var item in contractOpinionList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.TenderUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.TenderUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.ContractDeptOpinion == id)
            {
                List<ContractDeptOpinion> contractDeptOpinions = _iContractDeptOpinionRepository.Filter(x => x.OpinionUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (contractDeptOpinions.Count > 0)
                {
                    foreach (var item in contractDeptOpinions)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.OpinionUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.OpinionUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<ContractDeptOpinion> contractDeptOpinionList = _iContractDeptOpinionRepository.All().ToList();
                    foreach (var item in contractDeptOpinionList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.OpinionUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.OpinionUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.TenderSpecificationDeptOpinion == id)
            {
                List<TenderSpecDeptOpinion> tenderSpecDeptOpinions = _iTenderSpecDeptOpinionRepository.Filter(x => x.OpinionUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (tenderSpecDeptOpinions.Count > 0)
                {
                    foreach (var item in tenderSpecDeptOpinions)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.OpinionUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.OpinionUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<TenderSpecDeptOpinion> tenderSpecDeptOpinionList = _iTenderSpecDeptOpinionRepository.All().ToList();
                    foreach (var item in tenderSpecDeptOpinionList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.OpinionUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.OpinionUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.StaffRequirementOpinion == id)
            {
                List<StaffRequirementOpinion> staffRequirementOpinions = _iStaffRequirementOpinionRepository.Filter(x => x.TenderUrl.ToLower().Contains(fileName.ToLower())).ToList();
                if (staffRequirementOpinions.Count > 0)
                {
                    foreach (var item in staffRequirementOpinions)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.TenderUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.TenderUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<StaffRequirementOpinion> staffRequirementOpinionList = _iStaffRequirementOpinionRepository.All().ToList();
                    foreach (var item in staffRequirementOpinionList)
                    {
                        FormInfo formInfo = new FormInfo();
                        if (item.FileNo != 0)
                        {
                            formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        }
                        string filePath = item.TenderUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.TenderUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.StaffRequirementDeptOpinion == id)
            {
                List<StaffRequirementDeptOpinion> staffRequirementDeptOpinions = _iStaffRequirementDeptOpinionRepository.Filter(x => x.OpinionUrl.ToLower().Contains(fileName.ToLower())).ToList();

                if (staffRequirementDeptOpinions.Count > 0)
                {
                    foreach (var item in staffRequirementDeptOpinions)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.OpinionUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.OpinionUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<StaffRequirementDeptOpinion> staffRequirementDeptOpinionList = _iStaffRequirementDeptOpinionRepository.All().ToList();
                    foreach (var item in staffRequirementDeptOpinionList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.OpinionUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.OpinionUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.LettersendtoSFC == id || FormInformation.LettersendtoAFC == id)
            {
                List<CommitteeRaiseLetter> committeeRaiseLetters = _iCommitteeRaiseLetterRepository.Filter(x => x.ApproveUrl.ToLower().Contains(fileName.ToLower())).ToList();

                if (committeeRaiseLetters.Count > 0)
                {
                    foreach (var item in committeeRaiseLetters)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.ApproveUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.ApproveUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<CommitteeRaiseLetter> committeeRaiseLetterList = _iCommitteeRaiseLetterRepository.All().ToList();
                    foreach (var item in committeeRaiseLetterList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.ApproveUrl;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.ApproveUrl;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else if (FormInformation.LettersendtoFinanceMinistry == id)
            {
                List<FinanceMinistryLetter> financeMinistryLetters = _iFinanceMinistryLetterRepository.Filter(x => x.AttachedDocument.ToLower().Contains(fileName.ToLower())).ToList();

                if (financeMinistryLetters.Count > 0)
                {
                    foreach (var item in financeMinistryLetters)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.AttachedDocument;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.AttachedDocument;
                        formInfoes.Add(formInfo);
                    }
                }
                else
                {
                    List<FinanceMinistryLetter> financeMinistryLetterList = _iFinanceMinistryLetterRepository.All().ToList();
                    foreach (var item in financeMinistryLetterList)
                    {
                        FormInfo formInfo = new FormInfo();
                        formInfo.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.FileNo).FileNo;
                        string filePath = item.AttachedDocument;
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        formInfo.FileName = fileNameWithoutExtension;
                        formInfo.AttachedDocument = item.AttachedDocument;
                        formInfoes.Add(formInfo);
                    }
                }
                return formInfoes;
            }
            else
            {
                return formInfoList;
            }
        }
    }
}
