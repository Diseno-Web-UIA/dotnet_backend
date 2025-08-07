using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Utils;

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
        public async Task<ActionResult<IEnumerable<DTO.Telefono.GET>>> GetTelefonos(int personaId)
        {
            return Ok(await _context.Telefono
               .Include(t => t.Telefono_Tipo_idTelefonol_TipoNavigation)
               .Where(t => t.Persona_idPersona == personaId)
               .Select(t => new DTO.Telefono.GET
               {
                   idTelefono = t.idTelefono,
                   Numero = t.Numero,
                   Activo = t.Activo,
                   Fecha_Registro = t.Fecha_Registro,
                   Persona_idPersona = t.Persona_idPersona,
                   Telefono_Tipo_idTelefonol_Tipo = t.Telefono_Tipo_idTelefonol_Tipo
               })
               .ToListAsync());
        }

        // GET: api/telefono/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DTO.Telefono.GET>> GetTelefono(int id)
        {
            var t = await _context.Telefono
                .Include(t => t.Persona_idPersonaNavigation)
                .Include(t => t.Telefono_Tipo_idTelefonol_TipoNavigation)
                .FirstOrDefaultAsync(t => t.idTelefono == id);

            if (t == null) return NotFound(new Error("Telefono No Encontrado", 404));

            return Ok(new DTO.Telefono.GET {
                idTelefono = t.idTelefono,
                Numero = t.Numero,
                Activo = t.Activo,
                Fecha_Registro = t.Fecha_Registro,
                Persona_idPersona = t.Persona_idPersona,
                Telefono_Tipo_idTelefonol_Tipo = t.Telefono_Tipo_idTelefonol_Tipo
            });
        }

        // POST: api/telefono
        [HttpPost]
        public async Task<ActionResult<DTO.Telefono.GET>> CreateTelefono([FromBody] DTO.Telefono.POST dto)
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
        public async Task<ActionResult<DTO.Telefono.GET>> UpdateTelefono(int id, [FromBody] DTO.Telefono.PUT dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null) return NotFound(new Error("Teléfono no encontrado", 404));

            telefono.Numero = dto.Numero;
            telefono.Activo = dto.Activo ?? telefono.Activo;
            telefono.Fecha_Registro = dto.Fecha_Registro ?? telefono.Fecha_Registro;
            telefono.Persona_idPersona = dto.Persona_idPersona;
            telefono.Telefono_Tipo_idTelefonol_Tipo = dto.Telefono_Tipo_idTelefonol_Tipo;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new DTO.Telefono.GET
                {
                    idTelefono = telefono.idTelefono,
                    Numero = telefono.Numero,
                    Activo = telefono.Activo,
                    Fecha_Registro = telefono.Fecha_Registro,
                    Persona_idPersona = telefono.Persona_idPersona,
                    Telefono_Tipo_idTelefonol_Tipo = telefono.Telefono_Tipo_idTelefonol_Tipo
                });
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new Error(ex.Message, 409));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error("Error Inesperado", 500, ex.StackTrace ?? ""));
            }
        }

        //PATCH: api/telefono/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<DTO.Telefono.GET>> Patch(int id, [FromBody] DTO.Telefono.PATCH dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null) return NotFound(new Error("Teléfono No Encontrado", 404));
            if (dto.Numero != null) telefono.Numero = dto.Numero;
            if (dto.Activo.HasValue) telefono.Activo = dto.Activo.Value;
            if (dto.Fecha_Registro.HasValue) telefono.Fecha_Registro = dto.Fecha_Registro.Value;
            if (dto.Persona_idPersona.HasValue) telefono.Persona_idPersona = dto.Persona_idPersona.Value;
            if (dto.Telefono_Tipo_idTelefonol_Tipo.HasValue)
                telefono.Telefono_Tipo_idTelefonol_Tipo = dto.Telefono_Tipo_idTelefonol_Tipo.Value;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new DTO.Telefono.GET
                {
                    idTelefono = telefono.idTelefono,
                    Numero = telefono.Numero,
                    Activo = telefono.Activo,
                    Fecha_Registro = telefono.Fecha_Registro,
                    Persona_idPersona = telefono.Persona_idPersona,
                    Telefono_Tipo_idTelefonol_Tipo = telefono.Telefono_Tipo_idTelefonol_Tipo
                });
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new Error(ex.Message, 409));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error("Error Inesperado", 500, ex.StackTrace ?? ""));
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
    }
}