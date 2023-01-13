using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface ICompraService
    {
        Task<bool> Guardar(int ClienteID, List<int> articulosId);
    }
}
