using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoBecaController : Controller
    {
        public IActionResult GetAll()
        {
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            alumnoBeca.Alumno = new ML.Alumno();
            alumnoBeca.Beca = new ML.Beca();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/");
                var responseTask = client.GetAsync("AlumnoBeca");
                responseTask.Wait();

                var resultService = responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var itemAlumnoBeca in readTask.Result.Objects)
                    {
                        ML.AlumnoBeca resultAlumnoBeca = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.AlumnoBeca>(itemAlumnoBeca.ToString());
                        alumnoBeca.Alumno = resultAlumnoBeca.Alumno;
                        alumnoBeca.Beca = resultAlumnoBeca.Beca;
                        alumnoBeca.AlumnosBecas.Add(resultAlumnoBeca);
                        
                    }
                }
            }
            return View(alumnoBeca);
        }
    }
}
