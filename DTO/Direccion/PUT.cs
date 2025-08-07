using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Direccion
{
    public class PUT
    {
        [Required]
        public int idDireccion { get; set; }

        public int idTipo_Direccion { get; set; }
        public int Persona_idPersona { get; set; }

        [Required]
        [StringLength(250)]
        public required string Detalle_Direccion { get; set; }
    }
}
