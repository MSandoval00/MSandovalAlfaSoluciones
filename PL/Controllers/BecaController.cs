using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class BecaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll(int IdAlumno)
        {
            HttpContext.Session.SetString("IdAlumno", IdAlumno.ToString());
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
        
       
    }
}
