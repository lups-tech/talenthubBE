using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using talenthubBE.Mapping;
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
         public async Task<IActionResult> ScrapeSkills(String text)
        {
            var skills = await GetSkills();

            IEnumerable<SkillDTO> jobSkillList =
                from skill in skills
                select skill.Title;
        }
    }
}
