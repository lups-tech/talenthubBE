using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using talenthubBE.Mapping;
using talenthubBE.Models.Developers;

namespace talenthubBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly MvcDataContext _context;

        public DevelopersController(MvcDataContext context)
        {
            _context = context;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperDTO>>> GetDevelopers()
        {
          if (_context.Developers == null)
          {
              return NotFound();
          }
            var res = await _context.Developers.Include("Skills").ToListAsync();

            List<DeveloperDTO> developers = new();
            foreach (Developer developer in res)
            {
                developers.Add(developer.ToDevDTO());
            }

            return Ok(developers);
        }

        // GET: api/Developers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperDTO>> GetDeveloper(Guid id)
        {
          if (_context.Developers == null)
          {
              return NotFound();
          }
            var developer = await _context.Developers.Include("Skills").FirstOrDefaultAsync(d => d.Id == id);

            if (developer == null)
            {
                return NotFound();
            }

            DeveloperDTO developerDTO = developer.ToDevDTO();

            return Ok(developerDTO);
        }

        // PUT: api/Developers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloper(Guid id, Developer developer)
        {
            if (id != developer.Id)
            {
                return BadRequest();
            }

            _context.Entry(developer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Developers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Developer>> PostDeveloper(Developer developer)
        {
          if (_context.Developers == null)
          {
              return Problem("Entity set 'MvcDataContext.Developers'  is null.");
          }
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeveloper", new { id = developer.Id }, developer);
        }

        [HttpPatch("/developerSkills")]
        public async Task<ActionResult<DeveloperDTO>> AddDeveloperSkills(CreateDeveloperSkillsRequest request)
        {
            if (!DeveloperExists(request.developerId))
            {
                return NotFound();
            }
            Developer developer = _context.Developers
                .Include("Skills")
                .First(d => d.Id == request.developerId);
            
            var skillsToAdd = new List<Skill>();
            foreach (Guid skillId in request.selectedSkillIds)
            {
                var currentSkill = _context.Skills
                    .Single(skill => skill.Id == skillId);
                skillsToAdd.Add(currentSkill);
            }

            developer.Skills.AddRange(skillsToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeveloper", new { id = developer.Id }, developer.ToDevDTO());
        }

        // DELETE: api/Developers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(Guid id)
        {
            if (_context.Developers == null)
            {
                return NotFound();
            }
            var developer = await _context.Developers.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("/developerskills")]
        public async Task<IActionResult> DeleteDeveloperSkills(DeleteDeveloperSkillsRequest request)
        {
            if (!DeveloperExists(request.developerId))
            {
                return NotFound();
            }
            Developer developer = _context.Developers
                .Include("Skills")
                .First(d => d.Id == request.developerId);

            Skill skilltoRemove = developer.Skills
                .Single(skill => skill.Id == request.SkillId);
            
            if (skilltoRemove == null)
            {
                return NotFound();
            }
            developer.Skills.Remove(skilltoRemove);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeveloperExists(Guid id)
        {
            return (_context.Developers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
