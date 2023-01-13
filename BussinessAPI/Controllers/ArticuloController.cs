using Data;
using Data.DTO;
using Data.Request;
using Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BussinessAPI.Controllers
{
    [Authorize]
    [Route("api/Articulo")]
    [ApiController]
    public class ArticuloController : Controller
    {
        private IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<ArticuloDTO> items = await _articuloService.ObtenerListado();
                return Ok(new { mensaje = "OK", Listado = items });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("Tienda/{id}")]
        public async Task<ActionResult> GetByTiendaID([FromRoute] int id)
        {
            try
            {
                List<ArticuloDTO> articulos = await _articuloService.ObtenerListadoPorTiendaID(id);
                return Ok(new { mensaje = "OK", Listado = articulos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Guardar([FromBody] ArticuloRequest request)
        {
            try
            {
                Articulo model = new Articulo()
                {
                    Id = request.Id,
                    Codigo = request.Codigo,
                    Descripcion = request.Descripcion,
                    Imagen = request.Imagen,
                    Precio = request.Precio,
                    Stock = request.Stock,
                    Eliminado = false
                };


                bool user = await _articuloService.Guardar(model, request.TiendaId);

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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetDetail([FromRoute] int id)
        {
            try
            {
                ArticuloDTO user = await _articuloService.ObtenerPorID(id);
                return Ok(new { mensaje = "OK", Item = user });
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

                bool user = await _articuloService.Eliminar(id);

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
