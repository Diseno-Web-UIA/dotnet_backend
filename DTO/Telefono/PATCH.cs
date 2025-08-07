using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Telefono
{
    public class PATCH
    {
        public bool? Activo { get; set; }

        [StringLength(15)]
        public string? Numero { get; set; }

        public DateTime? Fecha_Registro { get; set; }

        public int? Persona_idPersona { get; set; }

        public int? Telefono_Tipo_idTelefonol_Tipo { get; set; }
    }
}