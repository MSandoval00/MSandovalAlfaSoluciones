using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class AlumnoBeca
    {
        public int IdAlumnoBeca { get; set; }
        public ML.Alumno Alumno { get; set; }
        public ML.Beca Beca { get; set; }
        public List<object> AlumnosBecas { get; set; }
    }
}
