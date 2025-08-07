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
        public async Task<ActionResult<Persona>> CreatePersona([FromBody] DTO.Persona.POST dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persona = new Persona
            {
                Nombre1 = dto.Nombre1,
                Apellido1 = dto.Apellido1,
                Apellido2 = dto.Apellido2,
                Fecha_Nacimiento = dto.Fecha_Nacimiento,
                genero = dto.genero
            };

            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.idPersona }, persona);
        }

        // PUT: api/persona/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, [FromBody] DTO.Persona.PUT dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound(new
                {
                    message = "Persona no encontrada",
                    status = 404
                });
            }

            persona.Nombre1 = dto.Nombre1;
            persona.Apellido1 = dto.Apellido1;
            persona.Apellido2 = dto.Apellido2;
            persona.Fecha_Nacimiento = dto.Fecha_Nacimiento;
            persona.genero = dto.genero;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(persona);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound(new
                    {
                        message = "Persona ya no existe (concurrency error)",
                        status = 404
                    });
                }

                throw;
            }
        }

        // PATCH: api/persona/{id}  
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPersona(int id, [FromBody] DTO.Persona.PATCH dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound(new
                {
                    message = "Persona no encontrada",
                    status = 404
                });
            }
            if (dto.Nombre1 != null) persona.Nombre1 = dto.Nombre1;
            if (dto.Apellido1 != null) persona.Apellido1 = dto.Apellido1;
            if (dto.Apellido2 != null) persona.Apellido2 = dto.Apellido2;
            if (dto.Fecha_Nacimiento != null) persona.Fecha_Nacimiento = (DateOnly)dto.Fecha_Nacimiento;
            if (dto.genero != null) persona.genero = (int)dto.genero;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(persona);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound(new
                    {
                        message = "Persona ya no existe (concurrency error)",
                        status = 404
                    });
                }
                throw;
            }
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