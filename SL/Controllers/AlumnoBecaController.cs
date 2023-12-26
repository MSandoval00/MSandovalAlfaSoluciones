using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoBecaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.AlumnoBeca alumnoBeca=new ML.AlumnoBeca();
            alumnoBeca.Beca=new ML.Beca();
            ML.Result result=BL.AlumnoBeca.GetAll(alumnoBeca);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("Busqueda")]
        public IActionResult GetAll(ML.AlumnoBeca alumnoBeca)
        {
            ML.Result result = BL.AlumnoBeca.GetAll(alumnoBeca);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("")]
        public IActionResult Add(ML.AlumnoBeca alumnoBeca)
        {
            ML.Result result = BL.AlumnoBeca.Add(alumnoBeca);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{IdBeca}")]
        public IActionResult Delete(int IdBeca)
        {
            ML.Result result = BL.AlumnoBeca.Delete(IdBeca);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
