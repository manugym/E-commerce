using TuringClothes.Database;
using TuringClothes.Pagination;
using TuringClothes.Enums;

namespace TuringClothes.Services
{
    public class CatalogService
    {
        private readonly MyDatabase _myDatabase;

        private readonly PagedList _pagination;
        public CatalogService(MyDatabase myDatabase, PagedList pagination)
        {
            _myDatabase = myDatabase;
            _pagination = pagination;
        }

        public async Task<PagedResults<Product>> GetPaginationCatalog(PaginationParams request)
        {

            var query = _myDatabase.Products.AsQueryable();
            
            // Aplica el ordenamiento basado en el parámetro 'OrderBy'
            query = request.OrderBy switch
            {
                OrderCatalog.OrderField.Price => request.Direction == OrderCatalog.OrderDirection.Ascending
                ? query.OrderBy(x => x.Price)
                : query.OrderByDescending(x => x.Price),
                OrderCatalog.OrderField.Name => request.Direction == OrderCatalog.OrderDirection.Ascending
                ? query.OrderBy(x => x.Name)
                : query.OrderByDescending(x => x.Name),
                _ => query // No hay orden si OrderBy es nulo
            };

            return await _pagination.CreatePagedGenericResults<Product>(query,
                request.PageNumber,
                request.PageSize,
                request.OrderBy,
                request.Direction
                );
        }

    }
}
