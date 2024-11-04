using TuringClothes.Database;
using TuringClothes.Enums;
using TuringClothes.Pagination;

namespace TuringClothes.Services
{
    public class CatalogService
    {
        private readonly MyDatabase _myDatabase;


        private readonly SmartSearchService _smartSearchService;
        public CatalogService(MyDatabase myDatabase, SmartSearchService smartSearchService)
        {
            _myDatabase = myDatabase;
            _smartSearchService = smartSearchService;
        }

        public PagedResults<Product> GetPaginationCatalog(PaginationParams request)
        {

            //var query = _myDatabase.Products.AsQueryable();
            var query = _smartSearchService.Search(request.Query);

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


            var skipAmount = request.PageSize * (request.PageNumber - 1);

            var totalNumberOfRecords = query.Count();

            // Luego aplica la paginación
            var results = query.Skip(skipAmount).Take(request.PageSize).ToList();

            var totalPageCount = (int)Math.Ceiling((double)totalNumberOfRecords / request.PageSize);

            return new PagedResults<Product>
            {
                Results = results,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };



        }


    }
}
