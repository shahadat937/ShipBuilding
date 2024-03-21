using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
   public class FinancialInstallmentManager : IFinancialInstallmentManager
   {
       private readonly IFinancialInstallmentRepository _financialInstallmentRepository;
       private readonly IProjectInstallmentRepository _projectInstallmentRepository;
       private readonly IBGPGRepository _bgpgRepository;
       public FinancialInstallmentManager(IFinancialInstallmentRepository financialInstallmentRepository, IProjectInstallmentRepository projectInstallmentRepository, IBGPGRepository bgpgRepository)
       {
           _financialInstallmentRepository = financialInstallmentRepository;
           _projectInstallmentRepository = projectInstallmentRepository;
           _bgpgRepository = bgpgRepository;
       }


       public List<FinancialInstallment> GetFinancialInstallment(long? projectId)
       {
           var ss = _financialInstallmentRepository.Filter(x => x.ProjectId == projectId).ToList();
           List<FinancialInstallment> financialInstallments = new List<FinancialInstallment>();
           foreach (var item in ss)
           {
               FinancialInstallment financialInstallment = new FinancialInstallment();
               financialInstallment.FinancialYearId = item.FinancialYearId;
               financialInstallment.FinancialInstallmentId = item.FinancialInstallmentId;
               financialInstallment.ProjectId = item.ProjectId;
               financialInstallment.Amount = _projectInstallmentRepository.Filter(x => x.FinancialInstallmentId == item.FinancialInstallmentId).Sum(x => (decimal?)x.Amount) ?? 0;
               financialInstallments.Add(financialInstallment);
               financialInstallment.Percentage = _projectInstallmentRepository.Filter(x => x.FinancialInstallmentId == item.FinancialInstallmentId).Sum(x => (int?)x.Percent) ?? 0;
           }
           return financialInstallments;
       }

       public void DeleteFinancialInstallmentByProjectId(long demandId)
       {
           _financialInstallmentRepository.Delete(x => x.ProjectId == demandId);
       }

       public string SaveFinancialInstallments(List<FinancialInstallment> financialInstallments, long demandId)
       {
           if (financialInstallments == null)
           {
               return "There is no article , please enter article";
           }
           foreach (var item in financialInstallments)
           {
            var ss =   _financialInstallmentRepository.FindOne(x => x.FinancialInstallmentId == item.FinancialInstallmentId);
               if (ss == null)
               {
                   FinancialInstallment financialInstallment = new FinancialInstallment();
                   //financialInstallment.FinancialInstallmentId = item.FinancialInstallmentId;
                   financialInstallment.FinancialYearId = item.FinancialYearId;
                   financialInstallment.ProjectId = demandId;

                   _financialInstallmentRepository.Save(financialInstallment); 
               }
                   
        
           }
           return "Data have been saved";
       }

       public List<FinancialInstallment> GetAllByProjectId(string projectId)
       {
         long proId = projectId != null ? Convert.ToInt64(projectId) : 0;
           return _financialInstallmentRepository.Filter(x =>x.ProjectId == proId).Include(x => x.ProjectInstallments).Include(x =>x.FinancialYear).Include(x =>x.Demand).ToList();
       }

       public void DeleteFinancialInstallment(long? financialID)
       {
         FinancialInstallment ss =  _financialInstallmentRepository.FindOne(x => x.FinancialInstallmentId == financialID);
           _financialInstallmentRepository.Delete(ss);
       }

       public string SaveBgpg(BGPG bgpg)
       {
           var oldData = _bgpgRepository.FindOne(x => x.FinancialInstallmentId == bgpg.FinancialInstallmentId);
           BGPG model = new BGPG();
           if (oldData != null)
           {
               oldData.GuranteeAmount = bgpg.GuranteeAmount;
               oldData.GuranteeDate = bgpg.GuranteeDate;
               model = oldData;
           }
           else
           {
               model.FinancialInstallmentId = bgpg.FinancialInstallmentId;
               model.GuranteeAmount = bgpg.GuranteeAmount;
               model.GuranteeDate = bgpg.GuranteeDate;
           }
           _bgpgRepository.Save(model);
           return "Saved Successfully";
       }
   }
}
