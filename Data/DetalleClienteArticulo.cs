using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DetalleClienteArticulo
    {
        public int Id { get; set; }
        public ICollection<Articulo> Articulos { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
    }
}
