using Data;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface ITiendaService
    {
        Task<List<TiendaDTO>> ObtenerListado();
        Task<bool> Guardar(Tienda model);
        Task<bool> Eliminar(int ID);
        Task<TiendaDTO> ObtenerPorID(int ID);
    }
}
