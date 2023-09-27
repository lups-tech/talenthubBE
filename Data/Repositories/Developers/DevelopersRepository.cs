using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using talenthubBE.Mapping;
using talenthubBE.Models;
using talenthubBE.Models.Developers;

namespace talenthubBE.Data
{
    public class DevelopersRepository : IDevelopersRepository
    {
        private readonly MvcDataContext _context;
        public DevelopersRepository(MvcDataContext context) => _context = context;
        public async Task<IEnumerable<DeveloperDTO>?> GetAllDevelopers()
        {
            if (_context.Developers == null)
          {
              return null;
          }
            var res = await _context.Developers.Include("Skills").ToListAsync();

            List<DeveloperDTO> developers = new();
            foreach (Developer developer in res)
            {
                developers.Add(developer.ToDevDTO());
            }

            return developers;
        }
        public async Task<DeveloperDTO?> GetDeveloper(Guid id)
        {
            if (_context.Developers == null)
            {
                return null;
            }
            var developer = await _context.Developers.Include("Skills").FirstOrDefaultAsync(d => d.Id == id);
            if (developer == null)
            {
                return null;
            }

            return developer.ToDevDTO();
        }
        public async Task<DeveloperDTO?> PutDeveloper(Guid id, Developer developer)
        {
            _context.Entry(developer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return developer.ToDevDTO();
        }
        public async Task<DeveloperDTO?> PostDeveloper(String userId, CreateDeveloperRequest request)
        {
             if (_context.Developers == null)
          {
              return null;
          }
            Organization org = _context.Organizations.Single(o=> o.Id == request.OrganizationId);
            Developer newDev = request.ToDev(org);
            User devsUser = _context.Users.Single(u => u.Id == userId);
            newDev.Users.Add(devsUser);
            
            _context.Developers.Add(newDev);
            await _context.SaveChangesAsync();

            return newDev.ToDevDTO();
        }
        public async Task DeleteDeveloper(Guid id)
        {
            if (_context.Developers == null)
            {
                throw new Exception("context not found");
            }
            var developer = _context.Developers.Find(id);
            if (developer == null)
            {
                return;
            }

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task<DeveloperDTO> AddDeveloperSkills(CreateDeveloperSkillsRequest request)
        {
            Developer developer = _context.Developers
                .Include("Skills")
                .First(d => d.Id == request.DeveloperId);
            
            var skillsToAdd = new List<Skill>();
            foreach (Guid skillId in request.SelectedSkillIds)
            {
                var currentSkill = _context.Skills
                    .Single(skill => skill.Id == skillId);
                skillsToAdd.Add(currentSkill);
            }

            developer.Skills.AddRange(skillsToAdd);
            await _context.SaveChangesAsync();

            return developer.ToDevDTO();
        }
        public async Task<bool> DeleteDeveloperSkill(DeleteDeveloperSkillsRequest request)
        {
            if (!DeveloperExists(request.DeveloperId))
            {
                return false;
            }
            Developer developer = _context.Developers
                .Include("Skills")
                .First(d => d.Id == request.DeveloperId);

            Skill skilltoRemove = developer.Skills
                .Single(skill => skill.Id == request.SkillId);
            
            if (skilltoRemove == null)
            {
                return false;
            }
            developer.Skills.Remove(skilltoRemove);
            await _context.SaveChangesAsync();

            return true;
        }
        private bool DeveloperExists(Guid id)
        {
            return (_context.Developers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}