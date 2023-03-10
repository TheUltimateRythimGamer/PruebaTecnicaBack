using Data;
using Data.DTO;
using Data.Request;
using Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BussinessAPI.Controllers
{
    [Authorize]
    [Route("api/Tienda")]
    [ApiController]
    public class TiendaController : Controller
    {
        private ITiendaService _tiendaService;

        public TiendaController(ITiendaService tiendaService)
        {
            _tiendaService = tiendaService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<TiendaDTO> users = await _tiendaService.ObtenerListado();
                return Ok(new { mensaje = "OK", Listado = users });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetDetail([FromRoute] int id)
        {
            try
            {
                TiendaDTO user = await _tiendaService.ObtenerPorID(id);
                return Ok(new { mensaje = "OK", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Guardar([FromBody] TiendaRequest request)
        {
            try
            {
                Tienda model = new Tienda()
                {
                    Id = request.Id,
                    Direccion = request.Direccion,
                    Sucursal = request.Sucursal,
                    Eliminado = false
                };


                bool user = await _tiendaService.Guardar(model); 

                if (user)
                    return Ok(new { mensaje = "OK" });
                else
                    return BadRequest(new { mensaje = "Error al guardar" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Eliminar([FromRoute] int id)
        {
            try
            {

                bool user = await _tiendaService.Eliminar(id);

                if (user)
                    return Ok(new { mensaje = "OK" });
                else
                    return BadRequest(new { mensaje = "Error al guardar" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
