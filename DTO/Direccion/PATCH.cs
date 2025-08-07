using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Direccion
{
    public class PATCH
    {
        public int? idTipo_Direccion { get; set; }

        public int? Persona_idPersona { get; set; }

        [StringLength(250)]
        public string? Detalle_Direccion { get; set; }
    }
}