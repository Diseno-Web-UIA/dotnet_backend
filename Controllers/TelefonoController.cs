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
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos(int personaId)
        {
            return await _context.Telefono
               .Include(t => t.Telefono_Tipo_idTelefonol_TipoNavigation)
               .Where(t => t.Persona_idPersona == personaId)
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

        // POST: api/telefono
        [HttpPost]
        public async Task<ActionResult<Telefono>> CreateTelefono([FromBody] DTO.Telefono.POST dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var telefono = new Telefono
            {
                Persona_idPersona = dto.Persona_idPersona,
                Telefono_Tipo_idTelefonol_Tipo = dto.Telefono_Tipo_idTelefonol_Tipo,
                Numero = dto.Numero,
                Activo = dto.Activo ?? true,
                Fecha_Registro = DateTime.UtcNow

            };

            _context.Telefono.Add(telefono);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTelefono), new { id = telefono.idTelefono }, telefono);
        }

        // PUT: api/telefono/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTelefono(int id, [FromBody] DTO.Telefono.PUT dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null) return NotFound(new
            {
                Message = "Telefono not found",
                StatusCode = 404
            });

            telefono.Numero = dto.Numero;
            telefono.Activo = dto.Activo ?? telefono.Activo;
            telefono.Fecha_Registro = dto.Fecha_Registro ?? telefono.Fecha_Registro;
            telefono.Persona_idPersona = dto.Persona_idPersona;
            telefono.Telefono_Tipo_idTelefonol_Tipo = dto.Telefono_Tipo_idTelefonol_Tipo;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(telefono);
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
        }

        //PATCH: api/telefono/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPersona(int id, [FromBody] DTO.Telefono.PATCH dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null) return NotFound(new
            {
                message = "Persona no encontrada",
                status = 404
            });
            if(dto.Numero != null) telefono.Numero = dto.Numero;
            if (dto.Activo.HasValue) telefono.Activo = dto.Activo.Value;
            if (dto.Fecha_Registro.HasValue) telefono.Fecha_Registro = dto.Fecha_Registro.Value;
            if (dto.Persona_idPersona.HasValue) telefono.Persona_idPersona = dto.Persona_idPersona.Value;
            if (dto.Telefono_Tipo_idTelefonol_Tipo.HasValue)
                telefono.Telefono_Tipo_idTelefonol_Tipo = dto.Telefono_Tipo_idTelefonol_Tipo.Value;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(telefono);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(id))
                {
                    return NotFound(new
                    {
                        message = "Teléfono ya no existe (concurrency error)",
                        status = 404
                    });
                }
                throw;
            }
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

            return Ok(new {
                message = "Teléfono eliminado correctamente",
                status = 200
            });
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefono.Any(e => e.idTelefono == id);
        }
    }
}