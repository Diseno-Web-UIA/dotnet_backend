namespace backend.DTO.Usuario
{
    public class GET
    {
        public required string idUsuario { get; set; }
        public byte estado { get; set; }
        public required string Clave { get; set; }
        public int Persona_idPersona { get; set; }
    }
}