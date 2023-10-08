using Microsoft.AspNetCore.Mvc;
using EspacioEntidades;
using Microsoft.AspNetCore.Http.HttpResults;

namespace cadeteriaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class cadeteriaController : ControllerBase
{
    private readonly ILogger<cadeteriaController> _logger;
    private Cadeteria cadeteria;
    public cadeteriaController(ILogger<cadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteriaSingleton();
    }


    [HttpGet (Name="cadeteria Nombre")]
    // [Route("cadeteria")]
    public ActionResult<string> getNombre()
    {
        if (cadeteria != null && cadeteria.Nombre != null)
        {
            return Ok(cadeteria.Nombre);
        }
        else
        {
            return NotFound(); 
        }
    }

    // [HttpGet]
    // [Route("cadetes")]
    // public List<Cadete> GetCadetes()
    // {
    //     lis
    //     return cadeteria.ListadoCadetes;
    // }

    // [HttpGet]
    // [Route("informe")]
    // public List<Pedido> GetPedidos()
    // {
    //     return cadeteria.ListadoPedidos;
    // }
}
