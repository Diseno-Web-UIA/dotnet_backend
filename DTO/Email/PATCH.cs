namespace backend.DTO.Email
{
    public class PATCH
    {
        public bool? Activo { get; set; }
        public bool? Verificado { get; set; }

        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Actualizacion { get; set; }

        public int? Tipo_Email_idTipo_Email { get; set; }
        public int? Persona_idPersona { get; set; }
    }
}