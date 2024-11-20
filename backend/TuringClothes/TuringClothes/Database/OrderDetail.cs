namespace TuringClothes.Database
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public long TemporaryOrderID { get; set; }
        public long ProductID { get; set; }
        public int Amount { get; set; }
        public Product? Product { get; set; }
    }
}
