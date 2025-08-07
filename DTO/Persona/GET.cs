namespace backend.DTO.Persona
{
    public class GET
    {
        public int idPersona { get; set; }
        public required string Nombre1 { get; set; }
        public required string Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public DateOnly Fecha_Nacimiento { get; set; }
        public int genero { get; set; }
        public List<Usuario.GET>? Usuarios { get; set; }
        public List<Telefono.GET>? Telefonos { get; set; }
        public List<Email.GET>? Emails { get; set; }
        public List<Direccion.GET>? Direcciones { get; set; }
    }
}