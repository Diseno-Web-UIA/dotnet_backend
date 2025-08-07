namespace backend.DTO.Persona
{
    public class PATCH
    {
        public string? Nombre1 { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public DateOnly? Fecha_Nacimiento { get; set; }
        public int? genero { get; set; }
    }
}
