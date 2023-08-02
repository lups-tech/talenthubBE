using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using talenthubBE.Mapping;
using talenthubBE.Models.Developers;
using talenthubBE.Models.Skills;

namespace talenthubBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly MvcDataContext _context;

        public SkillsController(MvcDataContext context)
        {
            _context = context;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDTO>>> GetSkills()
        {
          if (_context.Skills == null)
          {
              return NotFound();
          }
            var response = await _context.Skills.ToListAsync();
            List<SkillDTO> skills = new();
            foreach(Skill skill in response)
            {
                skills.Add(skill.ToSkillDTO());
            }
            return Ok(skills);
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDTO>> GetSkill(Guid id)
        {
          if (_context.Skills == null)
          {
              return NotFound();
          }
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill.ToSkillDTO());
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<SkillDTO>> PutSkill(Guid id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(skill.ToSkillDTO());
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillDTO>> PostSkill(Skill skill)
        {
          if (_context.Skills == null)
          {
              return Problem("Entity set 'MvcDataContext.Skills'  is null.");
          }
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkill", new { id = skill.Id }, skill.ToSkillDTO());
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            if (_context.Skills == null)
            {
                return NotFound();
            }
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillExists(Guid id)
        {
            return (_context.Skills?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("/scraper")]
         public async Task<ActionResult<SkillScraperResponse>> ScrapeSkills(String text)
        {
            var skillData = await _context.Skills.ToListAsync<Skill>();
            var skillQuery = skillData.Select(skill => regexGenerator(skill.Title));
            List<SkillDTO> jobSkills = new();
            int index = 0;
            foreach(Regex skill in skillQuery)
            {
                if(skill.Match(text).Success)
                {
                    jobSkills.Add(skillData[index].ToSkillDTO());
                }
                index++;
            }

            var devData = await _context.Developers.Include("Skills").ToListAsync<Developer>();
            
            List<DeveloperDTO> devByMatch = new();
            
            foreach(Developer dev in devData)
            {
                DeveloperDTO listedDev = dev.ToDevDTO();
                listedDev.skillMatch = jobSkills
                    .IntersectBy(dev.Skills.Select(s => s.Id), s => s.Id)
                    .Count();
                
                devByMatch.Add(listedDev);
            }
            
            return Ok(new SkillScraperResponse
                {
                    JobSkills = jobSkills,
                    Developers = devByMatch
                        .Where(dev => dev.skillMatch > 0)
                        .OrderByDescending(dev => dev.skillMatch)
                        .ToList()
                });
        }
        private Regex regexGenerator(string title)
        {
            return new Regex(pattern: title, RegexOptions.IgnoreCase);
        }
    }
}
