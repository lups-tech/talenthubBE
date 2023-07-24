using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace talenthubBE.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly MvcDeveloperContext _context;

        public DevelopersController(MvcDeveloperContext context)
        {
            _context = context;
        }

        // GET: api/Developers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDeveloper()
        {
          if (_context.Developers == null)
          {
              return NotFound();
          }
            return await _context.Developers.ToListAsync();
        }

        // GET: api/Developers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(Guid id)
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

            return developer;
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
              return Problem("Entity set 'MvcDevelopercontext.Developers'  is null.");
          }
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeveloper", new { id = developer.Id }, developer);
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

        private bool DeveloperExists(Guid id)
        {
            return (_context.Developers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
