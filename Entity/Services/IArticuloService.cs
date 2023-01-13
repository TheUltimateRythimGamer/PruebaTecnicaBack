using Data;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface IArticuloService
    {

        Task<List<ArticuloDTO>> ObtenerListado();
        Task<bool> Guardar(Articulo model, int tiendaId);
        Task<bool> Eliminar(int ID);
        Task<ArticuloDTO> ObtenerPorID(int ID);

        Task<List<ArticuloDTO>> ObtenerListadoPorTiendaID(int ID);
    }
}
