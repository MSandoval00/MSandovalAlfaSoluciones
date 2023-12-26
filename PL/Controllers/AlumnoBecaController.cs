using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoBecaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            alumnoBeca.Alumno = new ML.Alumno();
            alumnoBeca.Beca = new ML.Beca();
            alumnoBeca.AlumnosBecas = new List<object>();
            alumnoBeca.Beca.Becas=new List<object>();
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/");
                var becaTask = client.GetAsync("Beca");
                becaTask.Wait();
                var resultServiceBeca = becaTask.Result;
                if (resultServiceBeca.IsSuccessStatusCode)
                {
                    var readBecaTask = resultServiceBeca.Content.ReadAsAsync<ML.Result>();
                    readBecaTask.Wait();
                    foreach (var itemBeca in readBecaTask.Result.Objects)
                    {
                        ML.Beca resultBeca = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Beca>(itemBeca.ToString());
                        alumnoBeca.Beca.Becas.Add(resultBeca);
                    }
                }
            }
            var listaBecas = alumnoBeca.Beca.Becas;
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
            alumnoBeca.Beca.Becas = listaBecas;
            return View(alumnoBeca);
        }
        [HttpPost]
        public IActionResult GetAll(ML.AlumnoBeca alumnoBeca)
        {
            var IdBeca = alumnoBeca.Beca.IdBeca;
            alumnoBeca.Beca = new ML.Beca();
            alumnoBeca.Alumno = new ML.Alumno();
            alumnoBeca.Beca.IdBeca = IdBeca;
            alumnoBeca.AlumnosBecas = new List<object>();
            alumnoBeca.Beca.Becas = new List<object>();
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/");
                var becaTask = client.GetAsync("Beca");
                becaTask.Wait();
                
                var resultServiceBeca= becaTask.Result;
                if (resultServiceBeca.IsSuccessStatusCode)
                {
                    var readBecaTask = resultServiceBeca.Content.ReadAsAsync<ML.Result>();
                    readBecaTask.Wait();
                    foreach (var itemBeca in readBecaTask.Result.Objects)
                    {
                        ML.Beca resultBeca = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Beca>(itemBeca.ToString());
                        alumnoBeca.Beca.Becas.Add(resultBeca);
                    }
                }
            }
            var listaBecas = alumnoBeca.Beca.Becas;
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/AlumnoBeca/Busqueda");
                var postTask = client.PostAsJsonAsync("", alumnoBeca);
                postTask.Wait();

                var resultService = postTask.Result;
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
            alumnoBeca.Beca.Becas = listaBecas;
            return View(alumnoBeca);
        }
        [HttpPost]
        public IActionResult AddBeca(ML.Beca beca,int IdAlumno)
        {
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            alumnoBeca.Alumno = new ML.Alumno();
            alumnoBeca.Beca=new ML.Beca();
            alumnoBeca.Beca.IdBeca = beca.IdBeca;
            alumnoBeca.Alumno.IdAlumno = IdAlumno;
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/AlumnoBeca");
                var postTask = client.PostAsJsonAsync("", alumnoBeca);
                postTask.Wait();

                var resultPost=postTask.Result;
                if (resultPost.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se agrego correctamente la beca";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo agregar correctamente la beca";
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public IActionResult Delete(int IdBeca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/");
                var deleteTask = client.DeleteAsync("AlumnoBeca/" + IdBeca);
                deleteTask.Wait();

                var resultService = deleteTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se elimino correctamente la beca";
                }
                else
                {
                    ViewBag.Mensaje = "No se elimino correctamente la beca";
                }
                return PartialView("Modal");
            }
        }
    }
}
