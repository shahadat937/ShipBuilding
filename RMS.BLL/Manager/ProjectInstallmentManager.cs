using System.Data.Entity;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.Manager
{
    public class ProjectInstallmentManager : IProjectInstallmentManager
    {
        private readonly IProjectInstallmentRepository _iProjectInstallmentRepository;
        public ProjectInstallmentManager(IProjectInstallmentRepository iProjectInstallmentRepository)
        {
            _iProjectInstallmentRepository = iProjectInstallmentRepository;
        }

        public List<ProjectInstallment> GetProjectInstallmentsById(long projectId)
        {
            return
                _iProjectInstallmentRepository.Filter(x => x.FinancialInstallment.FinancialInstallmentId == projectId)
                    .Include(x => x.FinancialInstallment.Demand)
                    .ToList();

        }

        public ProjectInstallment GetProjectInstallmentById(long projectInstallmentId)
        {
            return _iProjectInstallmentRepository.FindOne(x => x.InstallmentId == projectInstallmentId);
        }
        public string SavePaymentInstallment(ProjectInstallment model)
        {
            ProjectInstallment projectInstallment = _iProjectInstallmentRepository.FindOne(x => x.InstallmentId == model.InstallmentId);
            if (projectInstallment == null)
            {
                ProjectInstallment p = new ProjectInstallment();
                //p.ProjectId = model.ProjectId;
                p.InstallmentNo = model.InstallmentNo;
                p.Amount = model.Amount;
                p.Term = model.Term;
                p.CreatedBy = PortalContext.CurrentUser.UserName;
                p.CreatedDate = DateTime.Now;
                p.UpdatedBy = PortalContext.CurrentUser.UserName;
                p.UpdatedDate = DateTime.Now;
                int result = _iProjectInstallmentRepository.Save(p);
                if (result > 0)
                {
                    return "Updated";
                }
                else
                {
                    return "Not Updated";
                }
            }
            else
            {
                //projectInstallment.ProjectId = model.ProjectId;
                projectInstallment.InstallmentNo = model.InstallmentNo;
                projectInstallment.Amount = model.Amount;
                projectInstallment.Term = model.Term;
                projectInstallment.UpdatedBy = PortalContext.CurrentUser.UserName;
                projectInstallment.UpdatedDate = DateTime.Now;
                int result = _iProjectInstallmentRepository.Save(projectInstallment);
                if (result > 0)
                {
                    return "Updated";
                }
                else
                {
                    return "Not Updated";
                }
            }
        }
        public void DeleteProjectInstallment(ProjectInstallment projectInstallment)
        {
             _iProjectInstallmentRepository.Delete(x => x.InstallmentId == projectInstallment.InstallmentId);
        }

        public void DeleteProjectInstallmentByProjectId(long? projectId)
        {
            throw new NotImplementedException();
        }


        public string SaveProjectInstallments(List<ProjectInstallment> models, long financialInstallmentId)
        {
            if (models == null)
            {
                return "There is no article , please enter article";
            }
            foreach (var item in models)
            {
                ProjectInstallment projectInstallment = new ProjectInstallment();
                projectInstallment.InstallmentNo = item.InstallmentNo;
                projectInstallment.Amount = item.Amount;
                projectInstallment.Term = item.Term;
                projectInstallment.Percent = item.Percent;
                projectInstallment.FinancialInstallmentId =financialInstallmentId;
                projectInstallment.CreatedBy = PortalContext.CurrentUser.UserName;
               // projectInstallment.CreatedDate = DateTime.Now;
                projectInstallment.UpdatedBy = PortalContext.CurrentUser.UserName;
                projectInstallment.UpdatedDate = DateTime.Now;
                _iProjectInstallmentRepository.Save(projectInstallment);
            }
            return "Data have been saved";
        }

        public decimal GetTotalInstallmentAmountByProjectId(long? projectId)
        {
            throw new NotImplementedException();
            //decimal InstallmentsAmount = 0;
            //List<ProjectInstallment> projectInstallments = _iProjectInstallmentRepository.All().Where(x => x.ProjectId == projectId).ToList();
            //foreach (var item in projectInstallments)
            //{
            //    InstallmentsAmount += Convert.ToDecimal(item.Amount);
            //}
            //return InstallmentsAmount;
        }


        public decimal GetInstallmentAmountById(long installmentId)
        {
            return _iProjectInstallmentRepository.FindOne(x => x.InstallmentId == installmentId).Amount;
        }

        public void DeleteProjectInstallmentByFinancialId(long financialInstallmentId)
        {
            _iProjectInstallmentRepository.Delete(x => x.FinancialInstallmentId == financialInstallmentId);
        }

        public decimal? GetProjectAmount(long? id)
        {
            return _iProjectInstallmentRepository.Filter(x => x.FinancialInstallment.ProjectId == id).Sum(x => (decimal?)x.Amount) ?? 0;
            
            
        }

        public void UpdateStatus(long? installmentId, DateTime? createDate)
        {
         ProjectInstallment ss =   _iProjectInstallmentRepository.FindOne(x => x.InstallmentId == installmentId);
            ss.Status = 10045;
            ss.CreatedDate = createDate;
            _iProjectInstallmentRepository.Save(ss);
        }
    }
}
