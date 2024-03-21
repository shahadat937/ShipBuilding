using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Web.Models.ViewModel;
using RMS.Utility;

namespace RMS.Web.Controllers
{
    public class ProjectController : Controller
    {

        private readonly IProjectManager _iProjectManager;
        private readonly ICommiteeManager _commiteeManager;
        private readonly IFinancialInstallmentManager _ifinancialinInstallmentManager;
        private readonly IContractSignManager _iContractSignManager;
        private readonly IProjectNoteManager _iprProjectNoteManager;
        public ProjectController(IProjectNoteManager _iprProjectNoteManager, IContractSignManager _iContractSignManager, IProjectManager iProjectManager, ICommiteeManager commiteeManager, IFinancialInstallmentManager ifinancialinInstallmentManager)
        {
            _iProjectManager = iProjectManager;
            _commiteeManager = commiteeManager;
            _ifinancialinInstallmentManager = ifinancialinInstallmentManager;
            this._iContractSignManager = _iContractSignManager;
            this._iprProjectNoteManager = _iprProjectNoteManager;
        }

        public ActionResult Index(string ProjectId,ProjectViewModel model)
        {
            
            model.ProjectId = ProjectId;
            int? id =   ProjectId != null ? Convert.ToInt32(ProjectId) : 0;
            model.ContractSign = _iContractSignManager.GetById(id);
            model.SpProjectTaskStatuse = _iProjectManager.GetProjectStatus(ProjectId);
            model.SpProjectTaskStatuse.TenderSpecificationChilds = _iProjectManager.GetTenderSpecChild(ProjectId);
            List<CommitteInfo> allCommiteeInfo = _commiteeManager.GetAllCommitteByProjectId(ProjectId);
            var commiteegrouped = from b in allCommiteeInfo
                                  group b by b.CommitteNameList.CommitteName into g
                                  select new Group<string, CommitteInfo> { Key = g.Key.ToString(), Values = g };

            model.AllCommitteListed = commiteegrouped.ToList();
            List<spProjectOpinion_Result> projectOpinion = _iProjectManager.GetAllOpinion(ProjectId);
          var  opinionsGrouped = from b in projectOpinion
                               group b by b.TaskName into g
                                  select new Group<string, spProjectOpinion_Result> { Key = g.Key, Values = g };
           model.AllOpinion = opinionsGrouped.ToList();

            model.FinancialInstallments = _ifinancialinInstallmentManager.GetAllByProjectId(ProjectId);
            model.ProjectNotes = _iprProjectNoteManager.GetAllbyId(id);
            return View(model);
        }

       

    }
}