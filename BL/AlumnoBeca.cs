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
        public static ML.Result GetAll(ML.AlumnoBeca alumnobeca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context = new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaAlumno in context.Alumnos
                                 join tablaAlumnoBeca in context.AlumnoBecas on tablaAlumno.IdAlumno equals tablaAlumnoBeca.IdAlumno into alumnoBecaJoin
                                 from tablaAlumnoBeca in alumnoBecaJoin.DefaultIfEmpty()
                                 join tablaBeca in context.Becas on tablaAlumnoBeca.IdBeca equals tablaBeca.IdBeca into becaJoin
                                 from tablaBeca in becaJoin.DefaultIfEmpty()
                                 where alumnobeca.Beca.IdBeca==0 || tablaBeca.IdBeca==alumnobeca.Beca.IdBeca
                                 select new
                                 {
                                     IdAlumno=tablaAlumno.IdAlumno,
                                     Nombre=tablaAlumno.Nombre,
                                     Genero=tablaAlumno.Genero,
                                     Email=tablaAlumno.Email,
                                     IdAlumnoBeca=tablaAlumnoBeca !=null ? tablaAlumnoBeca.IdAlumnoBeca:(int?)null,
                                     IdBeca=tablaBeca !=null ? tablaBeca.IdBeca:(int?)null,
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

                            if (row.IdAlumnoBeca==0 || row.IdBeca==0 || row.Tipo==null)
                            {
                                alumnoBeca.IdAlumnoBeca = 0;
                                alumnoBeca.Beca.IdBeca = 0;
                                alumnoBeca.Beca.Tipo = "No tiene beca asignada";
                            }
                            else
                            {
                                alumnoBeca.IdAlumnoBeca = int.Parse(row.IdAlumnoBeca.ToString());
                                alumnoBeca.Beca.IdBeca = int.Parse(row.IdBeca.ToString());
                                alumnoBeca.Beca.Tipo = row.Tipo;
                            }
                            //Beca
                            
                            result.Objects.Add(alumnoBeca);
                            result.Correct = true;

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ninguna";
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
        public static ML.Result Add(ML.AlumnoBeca alumnoBeca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context = new DL.MsandovalAlfaSolucionesContext())
                {
                    DL.AlumnoBeca nuevaBeca = new DL.AlumnoBeca();

                    nuevaBeca.IdAlumno = alumnoBeca.Alumno.IdAlumno;
                    nuevaBeca.IdBeca = alumnoBeca.Beca.IdBeca;
                    context.AlumnoBecas.Add(nuevaBeca);
                    context.SaveChanges();
                }
                result.Correct=true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdAlumoBeca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context = new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaAlumnoBeca in context.AlumnoBecas
                                 where tablaAlumnoBeca.IdAlumnoBeca == IdAlumoBeca
                                 select tablaAlumnoBeca).First();
                    context.AlumnoBecas.Remove(query);
                    context.SaveChanges();
                    result.Correct = true;
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
