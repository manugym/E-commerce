using TuringClothes.Database;

namespace TuringClothes.Model
{
    public class localCartOrderCollection
    {
        public ICollection<CartDetail> Details { get; set; }
    }
}
