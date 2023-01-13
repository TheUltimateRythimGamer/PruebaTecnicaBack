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
    public class ArticuloService : IArticuloService
    {
        private DataContext _context;

        public ArticuloService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Eliminar(int ID)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Articulo articuloDB = await _context.Articulos.Where(x => x.Id == ID).FirstOrDefaultAsync();
                    articuloDB.Eliminado = true;
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

        public async Task<bool> Guardar(Articulo model, int tiendaId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model.Id == 0)
                    {
                        model.Tienda = _context.Tiendas.Where(x => x.Id == tiendaId).FirstOrDefault();
                        await _context.Articulos.AddAsync(model); 
                    }
                    else
                    {
                        Articulo articuloDB = await _context.Articulos.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                        articuloDB.Codigo = model.Codigo;
                        articuloDB.Descripcion = model.Descripcion;
                        articuloDB.Precio = model.Precio;
                        articuloDB.Imagen = model.Imagen;
                        articuloDB.Stock = model.Stock;
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

        public async Task<List<ArticuloDTO>> ObtenerListado()
        {
            IEnumerable<Articulo> query = await _context.Articulos.Where(x => !x.Eliminado).ToListAsync();
            List<ArticuloDTO> list = query.Select(x =>
            {
                return new ArticuloDTO()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Imagen = x.Imagen,
                    Precio = x.Precio,
                    Stock = x.Stock
                };
            }).ToList();
            return list;
        }

        public async Task<List<ArticuloDTO>> ObtenerListadoPorTiendaID(int ID)
        {
            IEnumerable<Articulo> query = await _context.Articulos.Include(x => x.Tienda).Where(x => x.Tienda.Id == ID && !x.Eliminado).ToListAsync();
            List<ArticuloDTO> list = query.Select(x =>
            {
                return new ArticuloDTO()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Imagen = x.Imagen,
                    Precio = x.Precio,
                    Stock = x.Stock
                };
            }).ToList();
            return list;
        }

        public async Task<ArticuloDTO> ObtenerPorID(int ID)
        {
            Articulo query = await _context.Articulos.Where(x => x.Id == ID && !x.Eliminado).FirstOrDefaultAsync();
            ArticuloDTO item = new ArticuloDTO()
            {
                Id = query.Id,
                Codigo = query.Codigo,
                Descripcion = query.Descripcion,
                Imagen = query.Imagen,
                Precio = query.Precio,
                Stock = query.Stock
            };
            return item;
        }
    }
}
