
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TuringClothes.Extensions;
using TuringClothes.Enums;

namespace TuringClothes.Pagination
{
    public class PagedList
    {
        public async Task<PagedResults<T>> CreatePagedGenericResults<T>(IQueryable<T> queryable, int page, int pageSize, OrderCatalog.OrderField orderBy, OrderCatalog.OrderDirection direction)
        {
            var skipAmount = pageSize * (page - 1);

            var totalNumberOfRecords = await queryable.CountAsync();

            IQueryable<T> orderedQuery;

            // Si se proporciona el orden por el que ordenar
            if (orderBy != OrderCatalog.OrderField.None)
            {
                // Aplica la ordenación según el nombre de la propiedad y la dirección
                orderedQuery = queryable.OrderByPropertyOrField(orderBy.ToString(), direction == OrderCatalog.OrderDirection.Ascending);
            }
            else
            {
                // Si no hay orden, simplemente trabaja con el IQueryable original
                orderedQuery = queryable;
            }

            // Luego aplica la paginación
            var results = await orderedQuery.Skip(skipAmount).Take(pageSize).ToListAsync();

            var totalPageCount = (int)Math.Ceiling((double)totalNumberOfRecords / pageSize);

            return new PagedResults<T>
            {
                Results = results,
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords,
            };
    }
    }
}
