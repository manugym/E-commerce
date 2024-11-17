using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public class UnitOfWork : IUnitofWork
    {

        private readonly MyDatabase _context;
        private UserRepository _userRepository;
        private ProductRepository _productRepository;
        private CartRepository _cartRepository;

        public UnitOfWork(MyDatabase context)
        {
            _context = context;
        }
        public UserRepository UserRepository => _userRepository ?? new UserRepository(_context);

        public ProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);

        public CartRepository CartRepository => _cartRepository ?? new CartRepository(_context);
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
