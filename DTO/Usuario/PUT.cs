using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Usuario
{
    public class PUT
    {
        [Required]
        public byte estado { get; set; }

        [Required]
        [StringLength(50)]
        public required string Clave { get; set; }

        public int Persona_idPersona { get; set; }
    }
}
