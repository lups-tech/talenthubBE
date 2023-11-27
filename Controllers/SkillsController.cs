using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using talenthubBE.Data;
using talenthubBE.Helpers;
using talenthubBE.Models.Skills;

namespace talenthubBE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
         private readonly ISkillsRepository _repository;

        public SkillsController(ISkillsRepository skillsRepository)
        {
            _repository = skillsRepository;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDTO>>> GetSkills()
        {
            IEnumerable<SkillDTO>? response = await _repository.GetAllSkills();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDTO>> GetSkill(Guid id)
        {
            SkillDTO? response = await _repository.GetSkill(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
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
            SkillDTO? response = await _repository.PutSkill(id, skill);
            if(response == null)
            {
                return NotFound(new {message = "No such skill found"});
            }
            try
            {
                return Ok(response);
            }
            catch(Exception)
            {
                return Conflict(new {message = "There has been an issue handling your request"});
            }
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillDTO>> PostSkill(CreateSkillRequest request)
        {
            SkillDTO? response = await _repository.PostSkill(request);
            if(response == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetJob", new { id = response.Id }, response);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            try
            {
                await _repository.DeleteSkill(id);
            }
            catch (Exception e)
            {
                return NotFound(new {message = e.Message});
            }
            return NoContent();
        }

        [HttpPost("/scraper")]
         public async Task<ActionResult<SkillScraperResponse>> ScrapeSkills([FromBody] SkillScraperRequest text)
        {
            string orgId = ControllerHelper.OrgIdFinder(User);
            IEnumerable<SkillDTO> response = await _repository.ScrapeSkills(text);
            if(response.Count() < 0)
            {
                return NoContent();
            }
            SkillScraperResponse devMatch = await _repository.SkillMatchDevs(response, orgId);
            
            return Ok(devMatch);
        }
    }
}
