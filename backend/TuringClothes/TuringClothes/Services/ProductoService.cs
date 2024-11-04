using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;
using TuringClothes.Model;

namespace TuringClothes.Services
{
    public class ProductoService
    {
        private readonly MyDatabase _context;

        public ProductoService(MyDatabase context)
        {
            _context = context;
        }

        public async Task<List<Product>> FiltrarProductosAsync(ProductFilterDto filtros)
        {
            var query = _context.Products.AsQueryable();

            if (filtros.SearchField == SearchField.Nombre)
            {
                query = filtros.OrderDirection == OrderDirection.Descendente
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name);
            }
            else if (filtros.SearchField == SearchField.Precio)
            {
                
                var sortedQuery = filtros.OrderDirection == OrderDirection.Descendente
            ? query.Select(p => new { p.Id, p.Name, Precio = (double)p.Price }).OrderByDescending(p => p.Precio)
            : query.Select(p => new { p.Id, p.Name, Precio = (double)p.Price }).OrderBy(p => p.Precio);
                return await sortedQuery.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = (decimal)p.Precio 
                }).ToListAsync();
            }

            return await query.ToListAsync();
        }
    }
}