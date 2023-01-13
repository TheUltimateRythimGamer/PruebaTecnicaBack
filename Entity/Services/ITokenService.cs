using Data;
using Data.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface ITokenService
    {
        Task<Cliente> Login(LoginRequest model);
    }
}
