using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Utils;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionController(lastprojectContext context) : ControllerBase
    {
        private readonly lastprojectContext _context = context;

        // GET: api/direccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTO.Direccion.GET>>> GetDirecciones(int personaId)
        {

            return Ok(await _context.Direccion
                .Include(d => d.Persona_idPersonaNavigation)
                .Include(d => d.idTipo_DireccionNavigation)
                .Where(d => d.Persona_idPersona == personaId)
                .Select(d => GetDireccionValue(d))
            .ToListAsync());
        }

        // GET: api/direccion/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DTO.Direccion.GET>> GetDireccion(int id)
        {
            var direccion = await _context.Direccion
                .Include(d => d.Persona_idPersonaNavigation)
                .Include(d => d.idTipo_DireccionNavigation)
                .FirstOrDefaultAsync(d => d.idDireccion == id);

            if (direccion == null) return NotFound(new Error("Dirección no encontrada", 404));

            return Ok(GetDireccionValue(direccion));
        }

        // POST: api/direccion
        [HttpPost]
        public async Task<ActionResult<DTO.Direccion.GET>> CreateDireccion([FromBody] DTO.Direccion.POST dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var direccion = new Direccion
            {
                idTipo_Direccion = dto.idTipo_Direccion,
                Persona_idPersona = dto.Persona_idPersona,
                Detalle_Direccion = dto.Detalle_Direccion
            };
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDireccion), new { id = direccion.idDireccion }, direccion);
        }

        // PUT: api/direccion/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<DTO.Direccion.GET>> UpdateDireccion(int id, [FromBody] DTO.Direccion.PUT dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var direccion = await _context.Direccion.FindAsync(id);

            if (direccion == null) return NotFound(new Error("La dirección No existe", 400));

            direccion.idTipo_Direccion = dto.idTipo_Direccion;
            direccion.Persona_idPersona = dto.Persona_idPersona;
            direccion.Detalle_Direccion = dto.Detalle_Direccion;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(GetDireccionValue(direccion));
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
        [HttpPatch("{id}")]
        public async Task<ActionResult<DTO.Direccion.GET>> Patch(int id, [FromBody] DTO.Direccion.PATCH dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var direccion = await _context.Direccion.FindAsync(id);
            if (direccion == null)
            {
                return NotFound(new Error("Dirección Non encontrada", 404));
            }
            if(dto.Detalle_Direccion != null) direccion.Detalle_Direccion = dto.Detalle_Direccion;
            if(dto.idTipo_Direccion != null) direccion.idTipo_Direccion = (int)dto.idTipo_Direccion;
            if(dto.Persona_idPersona != null) direccion.Persona_idPersona = (int)dto.Persona_idPersona;


            try
            {
                await _context.SaveChangesAsync();
                return Ok(GetDireccionValue(direccion));
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

        private static DTO.Direccion.GET GetDireccionValue(Direccion d)
        {
            return new DTO.Direccion.GET
            {
                idDireccion = d.idDireccion,
                idTipo_Direccion = d.idTipo_Direccion,
                Persona_idPersona = d.Persona_idPersona,
                Detalle_Direccion = d.Detalle_Direccion
            };
        }
    }
}