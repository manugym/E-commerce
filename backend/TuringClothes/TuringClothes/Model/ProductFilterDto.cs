namespace TuringClothes.Model
{
    public enum SearchField
    {
        Nombre,
        Precio
    }

    public enum OrderDirection
    {
        Ascendente,
        Descendente
    }
    public class ProductFilterDto
    {
     public SearchField SearchField {  get; set; } //0 = nombre 1 = precio
     public OrderDirection OrderDirection { get; set; } = OrderDirection.Ascendente; //0 = ascendente 1 = descendente
    }
}
