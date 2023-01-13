using Data;
using Data.Request;
using Entity.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public class TokenService : ITokenService
    {
        private DataContext _context;

        public TokenService(DataContext context)
        {
            _context = context;
        }

        public async Task<Cliente> Login(LoginRequest model)
        {
            return await _context.Clientes.Where(x => x.Contrasenia == model.Contrasenia && x.Correo == model.Correo).FirstOrDefaultAsync();
        }
    }
}
