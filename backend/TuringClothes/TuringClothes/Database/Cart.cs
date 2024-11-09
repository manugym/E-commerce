namespace TuringClothes.Database
{
    public class Cart
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        ICollection<Product> Products { get; set; }
    }
}
