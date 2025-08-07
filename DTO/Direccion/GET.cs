namespace backend.DTO.Direccion
{
    public class GET
    {
        public int idDireccion { get; set; }
        public int idTipo_Direccion { get; set; }
        public int Persona_idPersona { get; set; }
        public string Detalle_Direccion { get; set; } = string.Empty;
    }
}