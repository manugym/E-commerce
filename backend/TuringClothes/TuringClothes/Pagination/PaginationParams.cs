using static TuringClothes.Enums.OrderCatalog;

namespace TuringClothes.Pagination
{
    public class PaginationParams
    {
        public string? Query { get; set; }

        //necesario para la lógica del PageSize
        private const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        /*private int _pageSize = 8;*/

        //lógica por si se necesita usar una paginación flexible de elementos. Posible paginación vista admin (mostrar users)
        public int PageSize = 8;
       /* {
            get => _pageSize;
            set => _pageSize = (value < MaxPageSize) ? value : MaxPageSize;
        }*/
        public OrderField OrderBy { get; set; } = OrderField.None;
        public OrderDirection Direction { get; set; } = OrderDirection.Ascending;
    }
}
