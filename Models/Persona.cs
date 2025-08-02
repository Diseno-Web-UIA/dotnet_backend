{
    [Key]
    public int idPersona { get; set; }

    [Required]
    [StringLength(45)]
    [Unicode(false)]
    public string Nombre1 { get; set; }

    [Required]
    [StringLength(45)]
    [Unicode(false)]
    public string Apellido1 { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string Apellido2 { get; set; }

    public DateOnly Fecha_Nacimiento { get; set; }

    public int genero { get; set; }

    [InverseProperty("Persona_idPersonaNavigation")]
    public virtual ICollection<Direccion> Direccion { get; set; } = new List<Direccion>();

    [InverseProperty("Persona_idPersonaNavigation")]
    public virtual ICollection<Email> Email { get; set; } = new List<Email>();

    [InverseProperty("Persona_idPersonaNavigation")]
    public virtual ICollection<Telefono> Telefono { get; set; } = new List<Telefono>();

    [InverseProperty("Persona_idPersonaNavigation")]
    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
} 