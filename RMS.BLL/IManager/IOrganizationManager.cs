using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IOrganizationManager
    {
        List<Organization> GetAllOrganizations();

        string SaveOrganization(Organization organization);

        Organization GetOrganizationById(long organizationId);

        string EditOrganization(Organization organization);

        int DeleteOrganization(Organization organization);

        List<Organization> GetOrganizationsBySearchKey(string searchKey);
    }
}
