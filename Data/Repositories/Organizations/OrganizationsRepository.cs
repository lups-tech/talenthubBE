using Microsoft.EntityFrameworkCore;
using talenthubBE.Data.Repositories.Organizations;
using talenthubBE.Mapping;
using talenthubBE.Models;
using talenthubBE.Models.Organizations;
using talenthubBE.Models.Users;

namespace talenthubBE.Data.Repositories
{
    public class OrganizationsRepository : IOrganizationRepository 
    {
        private readonly MvcDataContext _context;
        public OrganizationsRepository(MvcDataContext context) => _context = context;
        public async Task<IEnumerable<OrganizationDTO>?> GetAllOrganizations()
        {
            if (_context.Organizations == null)
            {
                return null;
            }
            var res = await _context.Organizations.Include("Developers").Include("Jobs").Include("Users").ToListAsync();

            List<OrganizationDTO> organizations = new();
            foreach (Organization org in res)
            {
                organizations.Add(org.ToOrganizationDTO());
            }

            return organizations;
        }

        public async Task<OrganizationDTO?> GetOrganization(string id)
        {
            if (_context.Organizations == null)
            {
                return null;
            }
            var org = await _context.Organizations.Include("Developers").Include("Jobs").Include("Users").FirstOrDefaultAsync(u => u.Id == id);
            if (org == null)
            {
                return null;
            }

            return org.ToOrganizationDTO();
        }

        public async Task<OrganizationDTO?> PostOrganization(string orgId)
        {
            if (_context.Organizations == null)
            {
                return null;
            }
            if(_context.Organizations.Any(u => u.Id == orgId))
            {
                return null;
            }
            Organization newOrg = new()
            {
                Id = orgId,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };
            _context.Organizations.Add(newOrg);
            await _context.SaveChangesAsync();

            return newOrg.ToOrganizationDTO();
        }

        public async Task<OrganizationDTO?> PutOrganization(string id, Organization organization)
        {
            _context.Entry(organization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return organization.ToOrganizationDTO();
        }
        public async Task DeleteOrganization(string id)
        {
            if (_context.Organizations == null)
            {
                throw new Exception("context not found");
            }
            var org = _context.Organizations.Find(id);
            if (org == null)
            {
                throw new Exception("Organization not found");
            }

            _context.Organizations.Remove(org);
            await _context.SaveChangesAsync();

            return;
        }
        private bool OrganizationExists(String id)
        {
            return (_context.Organizations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<OrganizationDTO?> AddUserToOrganization(string orgId, string userId)
        {
            if (_context.Organizations == null)
            {
                return null;
            }
            try
            {
                Organization org = await _context.Organizations.Include("Users").SingleAsync(org => org.Id == orgId);
                User user = await _context.Users.SingleAsync(user => user.Id == userId);
                org.Users.Add(user);
                await _context.SaveChangesAsync();
                return org.ToOrganizationDTO();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RemoveUserFromOrganization(string orgId, string userId)
        {
           if (_context.Organizations == null)
            {
                throw new Exception("context not found");
            }
            try
            {
                Organization org = await _context.Organizations.Include("Users").SingleAsync(org => org.Id == orgId);
                User user = await _context.Users.SingleAsync(user => user.Id == userId);
                org.Users.Remove(user);
                await _context.SaveChangesAsync();
                return;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<OrganizationDTO?> AddJobToOrganization(string orgId, Guid jobId)
        {
            if (_context.Organizations == null)
            {
                return null;
            }
            try
            {
                Organization org = await _context.Organizations.Include("Jobs").SingleAsync(org => org.Id == orgId);
                Job job = await _context.JobDescriptions.SingleAsync(job => job.Id == jobId);
                org.Jobs.Add(job);
                await _context.SaveChangesAsync();
                return org.ToOrganizationDTO();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RemoveJobFromOrganization(string orgId, Guid jobId)
        {
            if (_context.Organizations == null)
            {
                throw new Exception("context not found");
            }
            try
            {
                Organization org = await _context.Organizations.Include("Jobs").SingleAsync(org => org.Id == orgId);
                Job job = await _context.JobDescriptions.SingleAsync(job => job.Id == jobId);
                org.Jobs.Remove(job);
                await _context.SaveChangesAsync();
                return;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<OrganizationDTO?> AddDeveloperToOrganization(string orgId, Guid devId)
        {
            if (_context.Organizations == null)
            {
                return null;
            }
            try
            {
                Organization org = await _context.Organizations.Include("Developers").SingleAsync(org => org.Id == orgId);
                Developer developer = await _context.Developers.SingleAsync(developer => developer.Id == devId);
                org.Developers.Add(developer);
                await _context.SaveChangesAsync();
                return org.ToOrganizationDTO();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RemoveDeveloperFromOrganization(string orgId, Guid devId)
        {
            if (_context.Organizations == null)
            {
                 throw new Exception("context not found");
            }
            try
            {
                Organization org = await _context.Organizations.Include("Developers").SingleAsync(org => org.Id == orgId);
                Developer developer = await _context.Developers.SingleAsync(developer => developer.Id == devId);
                org.Developers.Remove(developer);
                await _context.SaveChangesAsync();
                return;
            }
            catch
            {
                throw;
            }
        }
    }
}