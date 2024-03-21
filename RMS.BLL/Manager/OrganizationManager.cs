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
    public class OrganizationManager : BaseManager, IOrganizationManager
    {
        private readonly IOrganizationRepository _iOrganizationRepository;
        public OrganizationManager(IOrganizationRepository iOrganizationRepository)
        {
            _iOrganizationRepository = iOrganizationRepository;
        }
        public List<Organization> GetAllOrganizations()
        {
            return _iOrganizationRepository.All().ToList();
        }
        public string SaveOrganization(Organization model)
        {
            Organization organization = new Organization();
            organization.OrganizationName = model.OrganizationName;
            organization.ShortName = model.ShortName;
            organization.Description = model.Description;
            organization.CreatedBy = PortalContext.CurrentUser.UserName;
            organization.CreatedDate = DateTime.Now;
            organization.UpdatedBy = PortalContext.CurrentUser.UserName;
            organization.UpdatedDate = DateTime.Now;
            int result = _iOrganizationRepository.Save(organization);
            if(result>0)
            {
                return "Organization successfully saved";
            }
            else
            {
                return "Organization not Saved";
            }
        }
        public Organization GetOrganizationById(long organizationId)
        {
           return _iOrganizationRepository.FindOne(x => x.OrganizationId == organizationId);
        }
        public string EditOrganization(Organization model)
        {
            Organization organization = _iOrganizationRepository.FindOne(x => x.OrganizationId == model.OrganizationId);
            organization.OrganizationName = model.OrganizationName;
            organization.ShortName = model.ShortName;
            organization.Description = model.Description;
            organization.UpdatedBy = PortalContext.CurrentUser.UserName;
            organization.UpdatedDate = DateTime.Now;
            int result= _iOrganizationRepository.Edit(organization);
            if(result>0)
            {
                return "Organization Updated successfully";
            }
            else
            {
                return "Organization not updated";
            }
        }
        public int DeleteOrganization(Organization model)
        {
            Organization organization = _iOrganizationRepository.FindOne(x => x.OrganizationId == model.OrganizationId);
            return _iOrganizationRepository.Delete(organization);
        }
        public List<Organization> GetOrganizationsBySearchKey(string searchKey)
        {
            return _iOrganizationRepository.Filter(x => x.OrganizationName.ToLower().Contains(searchKey.ToLower()) ||
                x.ShortName.ToLower().Contains(searchKey.ToLower())).ToList();
        }
    }
}
