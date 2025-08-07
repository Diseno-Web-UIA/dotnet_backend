namespace backend.DTO.Telefono
{
    public class GET
    {
        public int idTelefono { get; set; }
        public bool? Activo { get; set; }
        public string Numero { get; set; } = string.Empty;
        public DateTime? Fecha_Registro { get; set; }
        public int Persona_idPersona { get; set; }
        public int Telefono_Tipo_idTelefonol_Tipo { get; set; }
    }
}