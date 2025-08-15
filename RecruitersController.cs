using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruiterApi.Data;
using RecruiterApi.Models;

namespace RecruiterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecruitersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public RecruitersController(AppDbContext db) => _db = db;

        // GET: api/recruiters?search=rahul&company=bridge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recruiter>>> GetRecruiters([FromQuery] string? search, [FromQuery] string? company)
        {
            var query = _db.Recruiters.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r => r.Name.Contains(search) || r.ContactNumber.Contains(search));
            }
            if (!string.IsNullOrWhiteSpace(company))
            {
                query = query.Where(r => r.CompanyName.Contains(company));
            }

            var results = await query
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();

            return Ok(results);
        }

        // GET: api/recruiters/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Recruiter>> GetRecruiter(int id)
        {
            var recruiter = await _db.Recruiters.FindAsync(id);
            if (recruiter == null) return NotFound();
            return Ok(recruiter);
        }

        // POST: api/recruiters
        [HttpPost]
        public async Task<ActionResult<Recruiter>> CreateRecruiter([FromBody] Recruiter input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Recruiters.Add(input);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecruiter), new { id = input.Id }, input);
        }

        // PUT: api/recruiters/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRecruiter(int id, [FromBody] Recruiter update)
        {
            if (id != update.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exists = await _db.Recruiters.AnyAsync(r => r.Id == id);
            if (!exists) return NotFound();

            _db.Entry(update).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/recruiters/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecruiter(int id)
        {
            var recruiter = await _db.Recruiters.FindAsync(id);
            if (recruiter == null) return NotFound();
            _db.Recruiters.Remove(recruiter);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}