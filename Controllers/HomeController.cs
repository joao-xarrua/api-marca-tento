using Microsoft.AspNetCore.Mvc;

namespace MarcaTento.Controllers
{
    /* 
       Home controller geralmente é o primeiro
    controller para se ter uma rota padrão.
      Geralmente utilizada para fazer um ping
    de HealthCheck, uma checagem da disponibilidade
    da API de receber requisições
     */
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
