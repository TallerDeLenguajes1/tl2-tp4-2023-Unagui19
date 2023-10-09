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
        cadeteria = Cadeteria.GetInstancia();
    }


    [HttpGet (Name="cadeteria")]
    // [Route("cadeteria")]
    public ActionResult<string> getNombre()
    {
        if (cadeteria != null)
        {
            return Ok(cadeteria.Nombre);
        }
        else
        {
            return NotFound(); 
        }
    }


    [HttpGet]
    [Route("cadetes")]
    public ActionResult<IEnumerable<Cadete>> GetCadetes()
    {
        return Ok(cadeteria.ListadoCadetes);
    }

    [HttpGet]
    [Route("pedidos")]
    public ActionResult<IEnumerable<Pedido>> GetPedidos()
    {
        return Ok(cadeteria.ListadoPedidos);
    }

    [HttpGet("GetInforme", Name = "GetInforme")]
    public ActionResult<string> GetInforme()
    {
        var cadeteria = Cadeteria.GetInstancia();
        var informe = cadeteria.GetInforme();
        return Ok(informe);
    }



    [HttpPost]//("AgregarPedido/Pedido={pedido}")]
    [Route("AddPedido")]
    public ActionResult<string> AgregarPedido(Pedido pedido)
    {
        bool agregado = cadeteria.AgregarPedido(pedido);
        if(agregado){
            
            return Ok("Pedido agregado con exito");
        }else
        {
            return BadRequest("Error al agregar pedido");
        }
    }

    [HttpPut]
    [Route("AsignarPedido")]
    public ActionResult<bool> AsignarPedido(int idPedido, int idCadete)
    {
        // Pedido pedido = new Pedido();
        if(idPedido!=0 && idCadete!=0){
            cadeteria.AsignarCadeteAPedido(idCadete,idPedido);
            return Ok("pedido asignado con exito");
        }
        else{
            return NotFound("Numero de cadete o pedido incorrectos");
        }
    }

    [HttpPut]
    [Route("CambiarEstadoPedido")]
    public ActionResult<bool> CambiarEstadoPedido(int idPedido,int NuevoEstado)
    {
        if(cadeteria.BuscarporPedidoPorNumero(idPedido)!=null){
            cadeteria.CambiarEstado(idPedido,NuevoEstado);
            return Ok("Pedido cambiado de estadoi con exito");
        }
        else{
            return NotFound("Numero de pedido incorrecto");
        }
    }


    [HttpPut]
    [Route ("CambiarCadetePedido")] 
    public ActionResult<Pedido>CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        cadeteria.ReasignarCadete(idNuevoCadete,idPedido);
        return Ok("Pedido reasignado con exito");
    }

// ● [Put] CambiarEstadoPedido(int idPedido,int NuevoEstado)
// ● [Put] CambiarCadetePedido(int idPedido,int idNuevoCadete)
}
