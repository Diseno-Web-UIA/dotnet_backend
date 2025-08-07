namespace backend.DTO.Email
{
    public class GET
    {
        public string Direccion_Email { get; set; } = string.Empty;
        public bool? Activo { get; set; }
        public bool? Verificado { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Actualizacion { get; set; }
        public int Tipo_Email_idTipo_Email { get; set; }
        public int Persona_idPersona { get; set; }
    }
}