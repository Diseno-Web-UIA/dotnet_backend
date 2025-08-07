using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Usuario
{
    public class PATCH
    {
        public byte? estado { get; set; }

        [StringLength(50)]
        public string? Clave { get; set; }

        public int? Persona_idPersona { get; set; }
    }
}
