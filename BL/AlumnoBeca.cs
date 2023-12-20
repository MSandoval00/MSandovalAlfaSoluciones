using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoBeca
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context = new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaAlumnoBeca in context.AlumnoBecas.DefaultIfEmpty()
                                 join tablaAlumno in context.Alumnos on tablaAlumnoBeca.IdAlumno equals tablaAlumno.IdAlumno
                                 join tablaBeca in context.Becas on tablaAlumnoBeca.IdBeca equals tablaBeca.IdBeca
                                 select new
                                 {
                                     IdAlumno=tablaAlumno.IdAlumno,
                                     Nombre=tablaAlumno.Nombre,
                                     Genero=tablaAlumno.Genero,
                                     Email=tablaAlumno.Email,
                                     IdAlumnoBeca=tablaAlumnoBeca.IdAlumnoBeca,
                                     IdBeca=tablaBeca.IdBeca,
                                     Tipo=tablaBeca.Tipo,
                                 });
                    result.Objects = new List<object>();
                    if (query != null&&query.Count()>0)
                    {
                        foreach (var row in query)
                        {
                            ML.AlumnoBeca alumnoBeca=new ML.AlumnoBeca();
                            alumnoBeca.Alumno = new ML.Alumno();
                            alumnoBeca.Beca=new ML.Beca();

                            //Alumno
                            alumnoBeca.Alumno.IdAlumno = row.IdAlumno;
                            alumnoBeca.Alumno.Nombre = row.Nombre;
                            alumnoBeca.Alumno.Genero = bool.Parse(row.Genero.ToString());
                            alumnoBeca.Alumno.Email=row.Email;

                            alumnoBeca.IdAlumnoBeca = row.IdAlumnoBeca;
                            //Beca
                            alumnoBeca.Beca.IdBeca = row.IdBeca;
                            alumnoBeca.Beca.Tipo = row.Tipo;
                            result.Objects.Add(alumnoBeca);
                            result.Correct = true;

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de becas de alumnos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
