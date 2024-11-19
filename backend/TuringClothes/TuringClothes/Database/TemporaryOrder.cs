namespace TuringClothes.Database
{
    public class TemporaryOrder
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public ICollection<OrderDetail> Details { get; set; }
    }
}
