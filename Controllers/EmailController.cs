using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Utils;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController(lastprojectContext context) : ControllerBase
    {
        private readonly lastprojectContext _context = context;

        // GET: api/email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTO.Email.GET>>> GetEmails(int idPersona)
        {
            return Ok(await _context.Email
                .Include(e => e.Persona_idPersonaNavigation)
                .Include(e => e.Tipo_Email_idTipo_EmailNavigation)
                .Where(e => e.Persona_idPersona == idPersona)
                .Select(e => new DTO.Email.GET
                {
                    Direccion_Email = e.Direccion_Email,
                    Activo = e.Activo,
                    Verificado = e.Verificado,
                    Tipo_Email_idTipo_Email = e.Tipo_Email_idTipo_Email,
                    Persona_idPersona = e.Persona_idPersona,
                    Fecha_Actualizacion = e.Fecha_Actualizacion,
                    Fecha_Creacion = e.Fecha_Creacion
                })
                .ToListAsync());
        }

        // GET: api/email/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<DTO.Email.GET>> GetEmail(string email)
        {
            var emailEntity = await _context.Email
                .Include(e => e.Persona_idPersonaNavigation)
                .Include(e => e.Tipo_Email_idTipo_EmailNavigation)
                .FirstOrDefaultAsync(e => e.Direccion_Email == email);

            if (emailEntity == null) return NotFound(new Error("Email No Encontrado", 404));

            return Ok(new DTO.Email.GET{
                Direccion_Email = emailEntity.Direccion_Email,
                Activo = emailEntity.Activo,
                Verificado = emailEntity.Verificado,
                Tipo_Email_idTipo_Email = emailEntity.Tipo_Email_idTipo_Email,
                Persona_idPersona = emailEntity.Persona_idPersona,
                Fecha_Actualizacion = emailEntity.Fecha_Actualizacion,
                Fecha_Creacion = emailEntity.Fecha_Creacion
            });
        }

        // POST: api/email
        [HttpPost]
        public async Task<ActionResult<DTO.Email.GET>> CreateEmail([FromBody] DTO.Email.POST dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = new Email
            {
                Direccion_Email = dto.Direccion_Email,
                Activo = dto.Activo ?? true,
                Verificado = dto.Verificado ?? false,
                Tipo_Email_idTipo_Email = dto.Tipo_Email_idTipo_Email,
                Persona_idPersona = dto.Persona_idPersona,
                Fecha_Actualizacion = DateTime.UtcNow,
                Fecha_Creacion = DateTime.UtcNow
            };

            _context.Email.Add(email);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmail), new { email = email.Direccion_Email }, email);
        }

        // PUT: api/email/{email}
        [HttpPut("{email}")]
        public async Task<ActionResult<DTO.Direccion.GET>> UpdateEmail(string address, [FromBody] DTO.Email.PUT dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = await _context.Email.FindAsync(address);
            if (email == null) return NotFound(new Error("Email No Encontrado", 404));

            email.Direccion_Email = dto.Direccion_Email;
            email.Activo = dto.Activo ?? email.Activo;
            email.Verificado = dto.Verificado ?? email.Verificado;
            email.Tipo_Email_idTipo_Email = dto.Tipo_Email_idTipo_Email;
            email.Persona_idPersona = dto.Persona_idPersona;
            email.Fecha_Actualizacion = DateTime.UtcNow;
            email.Fecha_Creacion = dto.Fecha_Creacion ?? email.Fecha_Creacion;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new DTO.Email.GET
                {
                    Direccion_Email = email.Direccion_Email,
                    Activo = email.Activo,
                    Verificado = email.Verificado,
                    Tipo_Email_idTipo_Email = email.Tipo_Email_idTipo_Email,
                    Persona_idPersona = email.Persona_idPersona,
                    Fecha_Actualizacion = email.Fecha_Actualizacion,
                    Fecha_Creacion = email.Fecha_Creacion
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

        // PATCH: api/email/{id}  
        [HttpPatch("{address}")]
        public async Task<ActionResult<DTO.Direccion.GET>> Patch(string address, [FromBody] DTO.Email.PATCH dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = await _context.Email.FindAsync(address);
            if (email == null) return NotFound(new Error("Email No Encontrado", 404));

            if (dto.Activo.HasValue) email.Activo = dto.Activo.Value;
            if (dto.Verificado.HasValue) email.Verificado = dto.Verificado.Value;
            if (dto.Tipo_Email_idTipo_Email.HasValue) email.Tipo_Email_idTipo_Email = dto.Tipo_Email_idTipo_Email.Value;
            if (dto.Persona_idPersona.HasValue) email.Persona_idPersona = dto.Persona_idPersona.Value;
            email.Fecha_Actualizacion = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new DTO.Email.GET
                {
                    Direccion_Email = email.Direccion_Email,
                    Activo = email.Activo,
                    Verificado = email.Verificado,
                    Tipo_Email_idTipo_Email = email.Tipo_Email_idTipo_Email,
                    Persona_idPersona = email.Persona_idPersona,
                    Fecha_Actualizacion = email.Fecha_Actualizacion,
                    Fecha_Creacion = email.Fecha_Creacion
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
    }
} 