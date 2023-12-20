using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class BecaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Beca beca = new ML.Beca();
            beca.Becas = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/");
                var responseTask = client.GetAsync("Beca");
                responseTask.Wait();

                var resultService = responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var itemBeca in readTask.Result.Objects)
                    {
                        ML.Beca becaResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Beca>(itemBeca.ToString());
                        beca.Becas.Add(becaResult);
                    }

                }

            }
            return View(beca);
        }
        [HttpGet]
        public IActionResult BecasAsignadasGetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = new List<object>();
            ML.Beca beca = new ML.Beca();
            using (var client =new HttpClient())
            {
                client.BaseAddress = new Uri("");
                var responseTask = client.GetAsync("Beca");
                responseTask.Wait();

                var resultService = responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var itemAlumno in readTask.Result.Objects)
                    {
                        ML.Alumno alumnoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(itemAlumno.ToString());
                        alumno.Alumnos.Add(alumnoResult);
                    }  
                }
            }
            return View(alumno); 
        }
    }
}
