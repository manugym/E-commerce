using TuringClothes.Database;
using TuringClothes.Repository;

namespace TuringClothes.Model
{
    public class UnitOfWork
    {
        private readonly MyDatabase _database; // NO TENGO CLARO SI DEBERÍA DEJARLA PRIVADA O PÚBLICA PARA USARLA EN OTROS FICHEROS.

        public UserRepository _userRepository { get; init; }
        public ProductRepository _productRepository { get; init; }
        public CartRepository _cartRepository { get; init; }
        public AuthRepository _authRepository { get; init; }
        public OrderRepository _orderRepository { get; init; }
        public TemporaryOrderRepository _temporaryOrderRepository { get; init; }


        public UnitOfWork(MyDatabase database,
            UserRepository userRepository,
            ProductRepository productRepository,
            CartRepository cartRepository,
            AuthRepository authRepository,
            OrderRepository orderRepository,
            TemporaryOrderRepository temporaryOrderRepository)
        {
            _database = database;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _authRepository = authRepository;
            _orderRepository = orderRepository;
            _temporaryOrderRepository = temporaryOrderRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _database.SaveChangesAsync() > 0;
        }
    }
}
