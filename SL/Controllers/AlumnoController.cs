using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();
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
        public IActionResult Add([FromBody]ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{IdAlumno}")]
        public IActionResult Update(int IdAlumno,[FromBody]ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Update(alumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{IdAlumno}")]
        public IActionResult GetById(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{IdAlumno}")]
        public IActionResult Delete(int IdAlumno)
        {
            ML.Result result = BL.Alumno.Delete(IdAlumno);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
