using System;
using System.Collections.Generic;

namespace DL;

public partial class Alumno
{
    public int IdAlumno { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Genero { get; set; }

    public int? Edad { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<AlumnoBeca> AlumnoBecas { get; set; } = new List<AlumnoBeca>();
}
