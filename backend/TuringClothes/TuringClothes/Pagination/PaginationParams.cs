namespace TuringClothes.Pagination
{
    public class PaginationParams
    {
        //necesario para la lógica del PageSize
        private const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 8;

        //lógica por si se necesita usar una paginación flexible de elementos. Posible paginación vista admin (mostrar users)
        public int PageSize
        {
           get => _pageSize;
            set => _pageSize = (value < MaxPageSize) ? value : MaxPageSize;
        }
        public string? OrderBy { get; set; }
        public bool OrderAsc { get; set; } = true;
    }
}
