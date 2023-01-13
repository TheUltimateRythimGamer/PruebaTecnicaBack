using Data.Request;
using Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BussinessAPI.Controllers
{
    [Authorize]
    [Route("api/Compras")]
    [ApiController]
    public class ComprasController : Controller
    {
        private ICompraService _compraService;

        public ComprasController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpPost]
        public async Task<ActionResult> Guardar([FromBody] CompraRequest request)
        {
            try
            {
                var headers = Request.Headers;

                var token = Request.Headers["Authorization"].FirstOrDefault();
                var usuarioId = GetTokenInfo(token.Replace("Bearer ", ""))["UserId"];

                bool user = await _compraService.Guardar(int.Parse(usuarioId), request.ArticulosId);

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

        protected Dictionary<string, string> GetTokenInfo(string token)
        {
            var TokenInfo = new Dictionary<string, string>();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claims = jwtSecurityToken.Claims.ToList();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }

            return TokenInfo;
        }
    }
}
