using TuringClothes.Model;

namespace TuringClothes.Database
{
    public class TemporaryOrder
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public ICollection<TemporaryOrderDetail> Details { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
