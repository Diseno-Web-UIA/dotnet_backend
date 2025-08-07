using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Telefono
{
    public class PUT
    {
        [Required]
        public int idTelefono { get; set; }

        public bool? Activo { get; set; }

        [Required]
        [StringLength(15)]
        public required string Numero { get; set; }

        public DateTime? Fecha_Registro { get; set; }

        public int Persona_idPersona { get; set; }

        public int Telefono_Tipo_idTelefonol_Tipo { get; set; }
    }
}