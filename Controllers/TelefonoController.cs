using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefonoController : ControllerBase
    {
        private readonly lastprojectContext _context;

        public TelefonoController(lastprojectContext context)
        {
            _context = context;
        }

        // GET: api/telefono
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos()
        {
            return await _context.Telefono
                .Include(t => t.Persona_idPersonaNavigation)
                .Include(t => t.Telefono_Tipo_idTelefonol_TipoNavigation)
                .ToListAsync();
        }

        // GET: api/telefono/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(int id)
        {
            var telefono = await _context.Telefono
                .Include(t => t.Persona_idPersonaNavigation)
                .Include(t => t.Telefono_Tipo_idTelefonol_TipoNavigation)
                .FirstOrDefaultAsync(t => t.idTelefono == id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        // GET: api/telefono/persona/{personaId}
        [HttpGet("persona/{personaId}")]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonosByPersona(int personaId)
        {
            return await _context.Telefono
                .Include(t => t.Telefono_Tipo_idTelefonol_TipoNavigation)
                .Where(t => t.Persona_idPersona == personaId)
                .ToListAsync();
        }

        // POST: api/telefono
        [HttpPost]
        public async Task<ActionResult<Telefono>> CreateTelefono([FromBody] Telefono telefono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Telefono.Add(telefono);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTelefono), new { id = telefono.idTelefono }, telefono);
        }

        // PUT: api/telefono/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTelefono(int id, [FromBody] Telefono telefono)
        {
            if (id != telefono.idTelefono)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(telefono).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(id))
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

        // DELETE: api/telefono/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Telefono.Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefono.Any(e => e.idTelefono == id);
        }
    }
}