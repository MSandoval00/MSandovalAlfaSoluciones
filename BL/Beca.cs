using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Beca
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context = new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaBeca in context.Becas
                                 select new
                                 {
                                     IdBeca = tablaBeca.IdBeca,
                                     Tipo = tablaBeca.Tipo,
                                 });
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Beca beca = new ML.Beca();
                            beca.IdBeca = row.IdBeca;
                            beca.Tipo = row.Tipo;
                            result.Objects.Add(beca);
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de las becas";
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
