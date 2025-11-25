using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using joballo_api.Data;
using joballo_api.Models;

namespace joballo_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPositionsController : ControllerBase
    {
        private readonly JobContext _context;

        public JobPositionsController(JobContext context)
        {
            _context = context;
        }

        // GET: api/JobPositions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetJobPositions()
        {
            return await _context.JobPositions.ToListAsync();
        }

        // GET: api/JobPositions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobPosition>> GetJobPosition(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);

            if (jobPosition == null)
            {
                return NotFound();
            }

            return jobPosition;
        }

        // POST: api/JobPositions
        [HttpPost]
        public async Task<ActionResult<JobPosition>> PostJobPosition(JobPosition jobPosition)
        {
            _context.JobPositions.Add(jobPosition);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobPosition), new { id = jobPosition.Id }, jobPosition);
        }

        // PUT: api/JobPositions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobPosition(int id, JobPosition jobPosition)
        {
            if (id != jobPosition.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobPosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPositionExists(id))
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

        // DELETE: api/JobPositions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            _context.JobPositions.Remove(jobPosition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobPositionExists(int id)
        {
            return _context.JobPositions.Any(e => e.Id == id);
        }
    }
}