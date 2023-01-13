using Data;
using Entity.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public class CompraService : ICompraService
    {
        private DataContext _context;

        public CompraService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Guardar(int ClienteID, List<int> articulosId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    List<Articulo> articulos = await _context.Articulos.Where(x => articulosId.Contains(x.Id)).ToListAsync();
                    Cliente cliente = await _context.Clientes.Where(x => x.Id == ClienteID).FirstOrDefaultAsync();
                    DetalleClienteArticulo detalleCliente = new DetalleClienteArticulo()
                    {
                        Id = 0,
                        Articulos = articulos,
                        Cliente = cliente,
                        Eliminado = false,
                        Fecha = DateTime.Now,
                    };

                    await _context.DetalleClienteArticulo.AddAsync(detalleCliente);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
