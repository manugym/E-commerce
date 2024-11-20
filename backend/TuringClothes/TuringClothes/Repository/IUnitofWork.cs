using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public interface IUnitofWork
    {
        UserRepository UserRepository { get; }
        ProductRepository ProductRepository { get; }
        CartRepository CartRepository { get; }
        void SaveChanges();
    }
}
