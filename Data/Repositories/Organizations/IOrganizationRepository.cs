using talenthubBE.Models;
using talenthubBE.Models.Organizations;

namespace talenthubBE.Data.Repositories.Organizations
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrganizationDTO>?> GetAllOrganizations();
        Task<OrganizationDTO?> GetOrganization(String id);
        Task<OrganizationDTO?> PutOrganization(String id, Organization Organization);
        Task<OrganizationDTO?> PostOrganization(String orgId);
        Task DeleteOrganization(String id);
    }
}