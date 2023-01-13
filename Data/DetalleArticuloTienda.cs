using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DetalleArticuloTienda
    {
        public int Id { get; set; }
        public ICollection<Articulo> Articulos { get; set; }
        public Tienda Tienda { get; set; }
        public DateTime Fecha { get; set; }
        public bool Eliminado { get; set; }
    }
}
