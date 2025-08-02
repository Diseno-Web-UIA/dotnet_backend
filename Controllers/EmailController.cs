using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly lastprojectContext _context;

        public EmailController(lastprojectContext context)
        {
            _context = context;
        }

        // GET: api/email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmails()
        {
            return await _context.Email
                .Include(e => e.Persona_idPersonaNavigation)
                .Include(e => e.Tipo_Email_idTipo_EmailNavigation)
                .ToListAsync();
        }

        // GET: api/email/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<Email>> GetEmail(string email)
        {
            var emailEntity = await _context.Email
                .Include(e => e.Persona_idPersonaNavigation)
                .Include(e => e.Tipo_Email_idTipo_EmailNavigation)
                .FirstOrDefaultAsync(e => e.Direccion_Email == email);

            if (emailEntity == null)
            {
                return NotFound();
            }

            return emailEntity;
        }

        // GET: api/email/persona/{personaId}
        [HttpGet("persona/{personaId}")]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmailsByPersona(int personaId)
        {
            return await _context.Email
                .Include(e => e.Tipo_Email_idTipo_EmailNavigation)
                .Where(e => e.Persona_idPersona == personaId)
                .ToListAsync();
        }

        // POST: api/email
        [HttpPost]
        public async Task<ActionResult<Email>> CreateEmail([FromBody] Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Email.Add(email);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmail), new { email = email.Direccion_Email }, email);
        }

        // PUT: api/email/{email}
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateEmail(string email, [FromBody] Email emailEntity)
        {
            if (email != emailEntity.Direccion_Email)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(emailEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(email))
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

        // DELETE: api/email/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteEmail(string email)
        {
            var emailEntity = await _context.Email.FindAsync(email);
            if (emailEntity == null)
            {
                return NotFound();
            }

            _context.Email.Remove(emailEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmailExists(string email)
        {
            return _context.Email.Any(e => e.Direccion_Email == email);
        }
    }
} 