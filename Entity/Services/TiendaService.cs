using Data;
using Data.DTO;
using Entity.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public class TiendaService : ITiendaService
    {
        private DataContext _context;

        public TiendaService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Eliminar(int ID)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Tienda tiendaDB = await _context.Tiendas.Where(x => x.Id == ID).FirstOrDefaultAsync();
                    tiendaDB.Eliminado = true;

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

        public async Task<bool> Guardar(Tienda model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model.Id == 0)
                        await _context.Tiendas.AddAsync(model);
                    else
                    {
                        Tienda tiendaDB = await _context.Tiendas.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                        tiendaDB.Sucursal = model.Sucursal;
                        tiendaDB.Direccion = model.Direccion;
                    }

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

        public async Task<List<TiendaDTO>> ObtenerListado()
        {
            var query = await _context.Tiendas.Where(x => !x.Eliminado).ToListAsync();
            return query.Select(x =>
            {
                return new TiendaDTO
                {
                    Id = x.Id,
                    Direccion = x.Direccion,
                    Sucursal = x.Sucursal
                };
            }).ToList();
        }

        public async Task<TiendaDTO> ObtenerPorID(int ID)
        {
            var query = await _context.Tiendas.Where(x => x.Id == ID).FirstOrDefaultAsync();

            return new TiendaDTO
            {
                Id = query.Id,
                Direccion = query.Direccion,
                Sucursal = query.Sucursal
            };
        }
    }
}
