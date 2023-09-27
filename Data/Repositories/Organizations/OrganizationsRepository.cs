using Microsoft.EntityFrameworkCore;
using talenthubBE.Data.Repositories.Organizations;
using talenthubBE.Mapping;
using talenthubBE.Models;
using talenthubBE.Models.Organizations;

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
                return;
            }

            _context.Organizations.Remove(org);
            await _context.SaveChangesAsync();

            return;
        }
        private bool OrganizationExists(String id)
        {
            return (_context.Organizations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}