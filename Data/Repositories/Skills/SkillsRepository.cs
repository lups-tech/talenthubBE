using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using talenthubBE.Mapping;
using talenthubBE.Models.Developers;
using talenthubBE.Models.Skills;

namespace talenthubBE.Data
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly MvcDataContext _context;
        public SkillsRepository(MvcDataContext context) => _context = context;

        public async Task<IEnumerable<SkillDTO>?> GetAllSkills()
        {
            if (_context.Skills == null)
            {
                return null;
            }
            var response = await _context.Skills.ToListAsync();
            List<SkillDTO> skills = new();
            foreach(Skill skill in response)
            {
                skills.Add(skill.ToSkillDTO());
            }
            return skills;
        }
        public async Task<SkillDTO?> GetSkill(Guid id)
        {
            if (_context.Skills == null)
            {
                return null;
            }
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return null;
            }

            return skill.ToSkillDTO();
        }
        public async Task<SkillDTO?> PutSkill(Guid id, Skill skill)
        {
            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return skill.ToSkillDTO();
        }
        public async Task<SkillDTO?> PostSkill(CreateSkillRequest request)
        {
            if (_context.Skills == null)
            {
                return null;
            }
            Skill skillToAdd = request.ToSkill();
            _context.Skills.Add(skillToAdd);
            await _context.SaveChangesAsync();

            return skillToAdd.ToSkillDTO();
        }
        public async Task DeleteSkill(Guid id)
        {
            if (_context.Skills == null)
            {
                throw new Exception("context not found");
            }
            var skill = _context.Skills.Find(id);
            if (skill == null)
            {
                throw new Exception("skill not found");
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task<IEnumerable<SkillDTO>> ScrapeSkills(SkillScraperRequest text)
        {
            var skillData = await _context.Skills.ToListAsync<Skill>();
            var skillQuery = skillData.Select(skill => RegexGenerator(skill.Title));
            List<SkillDTO> jobSkills = new();
            int index = 0;
            foreach(Regex skill in skillQuery)
            {
                if(skill.Match(text.Description).Success)
                {
                    jobSkills.Add(skillData[index].ToSkillDTO());
                }
                index++;
            }
            return jobSkills;
        }
        public async Task<SkillScraperResponse?> SkillMatchDevs(IEnumerable<SkillDTO> jobSkills)
        {
            var devData = await _context.Developers.Include("Skills").ToListAsync<Developer>();
            
            List<DeveloperDTO> devByMatch = new();
            
            foreach(Developer dev in devData)
            {
                DeveloperDTO listedDev = dev.ToDevDTO();
                listedDev.SkillMatch = jobSkills
                    .IntersectBy(dev.Skills.Select(s => s.Id), s => s.Id)
                    .Count();
                
                devByMatch.Add(listedDev);
            }
            
            return new SkillScraperResponse
                {
                    JobSkills = jobSkills.ToList<SkillDTO>(),
                    Developers = devByMatch
                        .Where(dev => dev.SkillMatch > 0)
                        .OrderByDescending(dev => dev.SkillMatch)
                        .ToList()
                };
        }
        private bool SkillExists(Guid id)
        {
            return (_context.Skills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private Regex RegexGenerator(string title)
        {
            return new Regex(pattern: title, RegexOptions.IgnoreCase);
        }
    }
}