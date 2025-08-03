using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly lastprojectContext _context;

        public PersonaController(lastprojectContext context)
        {
            _context = context;
        }

        // GET: api/persona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            return await _context.Persona
                .Include(p => p.Email)
                .Include(p => p.Telefono)
                .Include(p => p.Direccion)
                .ToListAsync();
        }

        // GET: api/persona/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Persona
                .Include(p => p.Email)
                .Include(p => p.Telefono)
                .Include(p => p.Direccion)
                .FirstOrDefaultAsync(p => p.idPersona == id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // POST: api/persona
        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.idPersona }, persona);
        }

        // PUT: api/persona/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, [FromBody] Persona persona)
        {
            if (id != persona.idPersona)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // DELETE: api/persona/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.idPersona == id);
        }
    }
} 