using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Usuario
{
    public class POST
    {
        [Required]
        [StringLength(45)]
        public required string idUsuario { get; set; }

        [Required]
        public byte estado { get; set; }

        [Required]
        [StringLength(50)]
        public required string Clave { get; set; }

        public int Persona_idPersona { get; set; }
    }
}
