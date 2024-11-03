using Microsoft.EntityFrameworkCore;
using System;
using TuringClothes.Database;
using TuringClothes.Pagination;

namespace TuringClothes.Services
{
    public class CatalogService
    {
        private readonly MyDatabase _myDatabase;

        private readonly IPagedList _pagination;
        public CatalogService(MyDatabase myDatabase, IPagedList pagination)
        {
            _myDatabase = myDatabase;
            _pagination = pagination;
        }

        public async Task<PagedResults<Product>> GetPaginationCatalog(PaginationParams request) 
        {
            
            var query = _myDatabase.Products.AsQueryable();

            // Aplica el ordenamiento basado en el parámetro 'OrderBy'
            query = request.OrderBy?.ToLower() switch
                {
        
            "price" => request.OrderAsc 
                ? query.OrderBy(x => ((float)x.Price)) 
                : query.OrderByDescending(x => x.Price),
            "name" => request.OrderAsc 
                ? query.OrderBy(x => x.Name) 
                : query.OrderByDescending(x => x.Name),
            
        };

            return await _pagination.CreatePagedGenericResults<Product>(query,
                request.PageNumber,
                request.PageSize,
                request.OrderBy!,
                request.OrderAsc
                );
        }

    }
}
