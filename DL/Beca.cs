using System;
using System.Collections.Generic;

namespace DL;

public partial class Beca
{
    public int IdBeca { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<AlumnoBeca> AlumnoBecas { get; set; } = new List<AlumnoBeca>();
}
