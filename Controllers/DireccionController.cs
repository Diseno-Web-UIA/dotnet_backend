using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionController : ControllerBase
    {
        private readonly lastprojectContext _context;

        public DireccionController(lastprojectContext context)
        {
            _context = context;
        }

        // GET: api/direccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Direccion>>> GetDirecciones()
        {
            return await _context.Direccion
                .Include(d => d.Persona_idPersonaNavigation)
                .Include(d => d.idTipo_DireccionNavigation)
                .ToListAsync();
        }

        // GET: api/direccion/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Direccion>> GetDireccion(int id)
        {
            var direccion = await _context.Direccion
                .Include(d => d.Persona_idPersonaNavigation)
                .Include(d => d.idTipo_DireccionNavigation)
                .FirstOrDefaultAsync(d => d.idDireccion == id);

            if (direccion == null)
            {
                return NotFound();
            }

            return direccion;
        }

        // GET: api/direccion/persona/{personaId}
        [HttpGet("persona/{personaId}")]
        public async Task<ActionResult<IEnumerable<Direccion>>> GetDireccionesByPersona(int personaId)
        {
            return await _context.Direccion
                .Include(d => d.idTipo_DireccionNavigation)
                .Where(d => d.Persona_idPersona == personaId)
                .ToListAsync();
        }

        // POST: api/direccion
        [HttpPost]
        public async Task<ActionResult<Direccion>> CreateDireccion([FromBody] Direccion direccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDireccion), new { id = direccion.idDireccion }, direccion);
        }

        // PUT: api/direccion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDireccion(int id, [FromBody] Direccion direccion)
        {
            if (id != direccion.idDireccion)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionExists(id))
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

        // DELETE: api/direccion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccion(int id)
        {
            var direccion = await _context.Direccion.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }

            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DireccionExists(int id)
        {
            return _context.Direccion.Any(e => e.idDireccion == id);
        }
    }
}