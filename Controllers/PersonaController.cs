using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Utils;

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
        public async Task<ActionResult<IEnumerable<DTO.Persona.GET>>> GetPersonas()
        {
            var dbPersonas = await _context.Persona
                .Include(p => p.Email)
                .Include(p => p.Telefono)
                .Include(p => p.Direccion)
                .Include(p => p.Usuario)
                .ToListAsync();

            List<DTO.Persona.GET> personas = [.. dbPersonas.Select(p => GetPersonaValue(p))];

            return Ok(personas);
        }

        // GET: api/persona/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DTO.Persona.GET>> GetPersona(int id)
        {
            var p = await _context.Persona
                .Include(p => p.Email)
                .Include(p => p.Telefono)
                .Include(p => p.Direccion)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.idPersona == id);

            if (p == null) return NotFound(new Error("Persona No Encontrada", 404));

            return Ok(GetPersonaValue(p));
        }

        // POST: api/persona
        [HttpPost]
        public async Task<ActionResult<DTO.Persona.GET>> CreatePersona([FromBody] DTO.Persona.POST dto)
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
        public async Task<ActionResult<DTO.Persona.GET>> UpdatePersona(int id, [FromBody] DTO.Persona.PUT dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await _context.Persona.FindAsync(id);
            if (persona == null) return NotFound(new Error("Persona No Encontrada", 404));

            persona.Nombre1 = dto.Nombre1;
            persona.Apellido1 = dto.Apellido1;
            persona.Apellido2 = dto.Apellido2;
            persona.Fecha_Nacimiento = dto.Fecha_Nacimiento;
            persona.genero = dto.genero;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(GetPersonaValue(persona));
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

        // PATCH: api/persona/{id}  
        [HttpPatch("{id}")]
        public async Task<ActionResult<DTO.Persona.GET>> PatchPersona(int id, [FromBody] DTO.Persona.PATCH dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null) return NotFound(new Error("Persona No Encontrada", 404));
            if (dto.Nombre1 != null) persona.Nombre1 = dto.Nombre1;
            if (dto.Apellido1 != null) persona.Apellido1 = dto.Apellido1;
            if (dto.Apellido2 != null) persona.Apellido2 = dto.Apellido2;
            if (dto.Fecha_Nacimiento != null) persona.Fecha_Nacimiento = (DateOnly)dto.Fecha_Nacimiento;
            if (dto.genero != null) persona.genero = (int)dto.genero;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(GetPersonaValue(persona));
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

        private static DTO.Persona.GET GetPersonaValue(Persona p)
        {
            return new DTO.Persona.GET
            {
                idPersona = p.idPersona,
                Nombre1 = p.Nombre1,
                Apellido1 = p.Apellido1,
                Apellido2 = p.Apellido2,
                Fecha_Nacimiento = p.Fecha_Nacimiento,
                genero = p.genero,
                Usuarios = [.. p.Usuario.Select(u => new DTO.Usuario.GET
                {
                    idUsuario = u.idUsuario,
                    estado = u.estado,
                    Clave = u.Clave,
                    Persona_idPersona = u.Persona_idPersona
                })],
                Emails = [.. p.Email.Select(e => new DTO.Email.GET
                {
                    Direccion_Email = e.Direccion_Email,
                    Activo = e.Activo,
                    Verificado = e.Verificado,
                    Tipo_Email_idTipo_Email = e.Tipo_Email_idTipo_Email,
                    Fecha_Actualizacion = e.Fecha_Actualizacion,
                    Fecha_Creacion = e.Fecha_Creacion
                })],
                Telefonos = [.. p.Telefono.Select(t => new DTO.Telefono.GET
                {
                    idTelefono = t.idTelefono,
                    Numero = t.Numero,
                    Activo = t.Activo,
                })],
                Direcciones = [.. p.Direccion.Select(d => new DTO.Direccion.GET
                {
                    idDireccion = d.idDireccion,
                    Detalle_Direccion = d.Detalle_Direccion,
                    idTipo_Direccion = d.idTipo_Direccion,
                })]
            };
        }
    }
} 