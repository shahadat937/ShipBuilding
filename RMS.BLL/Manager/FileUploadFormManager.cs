using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RMS.BLL.Manager
{
    public class FileUploadFormManager : BaseManager, IFileUploadFormManager
    {
        private readonly IFileUploadFormRepository _iFileUploadFormRepository;
        private readonly IDemandRepository _iDemandRepository;
        public FileUploadFormManager(IFileUploadFormRepository iFileUploadFormRepository, IDemandRepository iDemandRepository)
        {
            _iFileUploadFormRepository = iFileUploadFormRepository;
            _iDemandRepository = iDemandRepository;
        }
        public List<FileUploadForm> GetAll()
        {
            List<FileUploadForm> fileUploadForms = _iFileUploadFormRepository.All().Where(x=>x.IsActive==true).ToList();
            List<FileUploadForm> fileUploadFormList = new List<FileUploadForm>();
            foreach (var item in fileUploadForms)
            {
                FileUploadForm fileUploadForm = new FileUploadForm();
                fileUploadForm.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == item.ProjectId).FileNo;

                var TE_A = item.TEA;
                string tea = Path.GetFileNameWithoutExtension(TE_A);
                fileUploadForm.TEA = tea;
                fileUploadForm.TE_APDF = item.TEA;

                var fatSchedule = item.FATSchedule;
                string fatSche = Path.GetFileNameWithoutExtension(fatSchedule);
                fileUploadForm.FATSchedule = fatSche;
                fileUploadForm.FATSchedulePDF = item.FATSchedule;

                var fatProcedure = item.FATProcedure;
                string fatProce = Path.GetFileNameWithoutExtension(fatProcedure);
                fileUploadForm.FATProcedure = fatProce;
                fileUploadForm.FATProcedurePDF = item.FATProcedure;

                var fatReport = item.FATReport;
                string fatRepo = Path.GetFileNameWithoutExtension(fatReport);
                fileUploadForm.FATReport = fatRepo;
                fileUploadForm.FATReportPDF = item.FATReport;

                var hatSat = item.HATSAT;
                string hatSatWithoutExt = Path.GetFileNameWithoutExtension(hatSat);
                fileUploadForm.HATSAT = hatSatWithoutExt;
                fileUploadForm.HATSATPDF = hatSat;

                var pit = item.PIT;
                string pitWithoutExt = Path.GetFileNameWithoutExtension(pit);
                fileUploadForm.PIT = pitWithoutExt;
                fileUploadForm.PITPDF = pit;

                var letterGo = item.LetterGo;
                string letterWithoutExt = Path.GetFileNameWithoutExtension(letterGo);
                fileUploadForm.LetterGo = letterWithoutExt;
                fileUploadForm.LETTERPDF = letterGo;

                var approveGo = item.ApprovedGo;
                string approveWithoutExt = Path.GetFileNameWithoutExtension(approveGo);
                fileUploadForm.ApprovedGo = approveWithoutExt;
                fileUploadForm.APPROVEPDF = approveGo;

                fileUploadForm.FileUploadFormId = item.FileUploadFormId;
                fileUploadFormList.Add(fileUploadForm);
            }
            return fileUploadFormList;
        }
        public string SaveFileUploadForms(FileUploadForm model)
        {
            int result = 0;
            FileUploadForm fileUploadForm = _iFileUploadFormRepository.FindOne(x => x.ProjectId == model.ProjectId);
            if (fileUploadForm == null)
            {
                FileUploadForm newfileUploadForm = new FileUploadForm();
                newfileUploadForm.ProjectId = model.ProjectId;
                newfileUploadForm.TEA = model.TEA;
                newfileUploadForm.FATSchedule = model.FATSchedule;
                newfileUploadForm.FATProcedure = model.FATProcedure;
                newfileUploadForm.FATReport = model.FATReport;
                newfileUploadForm.HATSAT = model.HATSAT;
                newfileUploadForm.PIT = model.PIT;
                newfileUploadForm.CreatedBy = PortalContext.CurrentUser.UserName;
                newfileUploadForm.CreatedDate = DateTime.Now;
                newfileUploadForm.UpdatedBy = PortalContext.CurrentUser.UserName;
                newfileUploadForm.UpdatedDate = DateTime.Now;
                newfileUploadForm.IsActive = true;
                result = _iFileUploadFormRepository.Save(newfileUploadForm);
                if (result > 0)
                {
                    return "Saved successfully";
                }
                else
                {
                    return " Not Saved";
                }
            }
            else
            {
                fileUploadForm.ProjectId = model.ProjectId;
                if (model.TEA != null)
                {
                    fileUploadForm.TEA = model.TEA;
                }
                if (model.FATSchedule != null)
                {
                    fileUploadForm.FATSchedule = model.FATSchedule;
                }
                if (model.FATProcedure != null)
                {
                    fileUploadForm.FATProcedure = model.FATProcedure;
                }
                if (model.FATReport != null)
                {
                    fileUploadForm.FATReport = model.FATReport;
                }
                if (model.HATSAT != null)
                {
                    fileUploadForm.HATSAT = model.HATSAT;
                }
                if (model.PIT != null)
                {
                    fileUploadForm.PIT = model.PIT;
                }
                fileUploadForm.UpdatedBy = PortalContext.CurrentUser.UserName;
                fileUploadForm.UpdatedDate = DateTime.Now;
                result = _iFileUploadFormRepository.Save(fileUploadForm);
                if (result > 0)
                {
                    return "Updated  successfully";
                }
                else
                {
                    return " Not Updated ";
                }
            }
        }
        public FileUploadForm GetFileUploadForm(long fileUploadFormId)
        {
            FileUploadForm fileUploadFormNew = new FileUploadForm();
            FileUploadForm fileUploadForm = _iFileUploadFormRepository.FindOne(x => x.FileUploadFormId == fileUploadFormId);
            if (fileUploadForm != null)
            {
                fileUploadFormNew.FileUploadFormId = fileUploadForm.FileUploadFormId;
                var tea = fileUploadForm.TEA;
                string teaNew = Path.GetFileNameWithoutExtension(tea);
                fileUploadFormNew.TEA = teaNew + ".pdf";

                var fatSchedule = fileUploadForm.FATSchedule;
                string fatScheduleNew = Path.GetFileNameWithoutExtension(fatSchedule);
                fileUploadFormNew.FATSchedule = fatScheduleNew + ".pdf";

                var fatProcedure = fileUploadForm.FATProcedure;
                string fatProcedureNew = Path.GetFileNameWithoutExtension(fatProcedure);
                fileUploadFormNew.FATProcedure = fatProcedureNew + ".pdf";

                var fatReport = fileUploadForm.FATReport;
                string fatReportNew = Path.GetFileNameWithoutExtension(fatReport);
                fileUploadFormNew.FATReport = fatReportNew + ".pdf"; ;

                var hatSAT = fileUploadForm.HATSAT;
                string hatSATNew = Path.GetFileNameWithoutExtension(hatSAT);
                fileUploadFormNew.HATSAT = hatSATNew + ".pdf"; ;

                fileUploadFormNew.ProjectName = _iDemandRepository.FindOne(x => x.DemandId == fileUploadForm.ProjectId).FileNo;
                return fileUploadFormNew;
            }
            else
            {
                return fileUploadFormNew;
            }
        }

        public void DeleteFileUploadForm(long fileUploadFormId)
        {
            FileUploadForm fileUploadFormNew = _iFileUploadFormRepository.FindOne(x => x.FileUploadFormId == fileUploadFormId);
            if(fileUploadFormNew!=null)
            {
                fileUploadFormNew.IsActive = false;
                _iFileUploadFormRepository.Save(fileUploadFormNew);
            }
        }
        public FileUploadForm GetFileUploadFormByID(long fileUploadFormId)
        {
            if(fileUploadFormId<0)
            {
                return new FileUploadForm();
            }           
            FileUploadForm fileUploadForm = _iFileUploadFormRepository.FindOne(x => x.FileUploadFormId == fileUploadFormId);
            if (fileUploadForm == null)
            {
                return new FileUploadForm();
            }
            else
            {
                return fileUploadForm;
            }
        }
    }
}
