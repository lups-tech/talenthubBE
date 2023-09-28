using talenthubBE.Models;
using talenthubBE.Models.Organizations;
using talenthubBE.Models.Users;

namespace talenthubBE.Data.Repositories.Organizations
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrganizationDTO>?> GetAllOrganizations();
        Task<OrganizationDTO?> GetOrganization(String id);
        Task<OrganizationDTO?> PutOrganization(String id, Organization Organization);
        Task<OrganizationDTO?> PostOrganization(String orgId);
        Task DeleteOrganization(String id);
        Task<OrganizationDTO?> AddUserToOrganization(String orgId, String userId);
        Task RemoveUserFromOrganization(String orgId, String userId);
        Task<OrganizationDTO?> AddJobToOrganization(String orgId, Guid jobId);
        Task RemoveJobFromOrganization(String orgId, Guid jobId);
        Task<OrganizationDTO?> AddDeveloperToOrganization(String orgId, Guid devId);
        Task RemoveDeveloperFromOrganization(String orgId, Guid devId);
    }
}