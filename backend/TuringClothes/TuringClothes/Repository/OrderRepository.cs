using System.Security.Cryptography.X509Certificates;
using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public class OrderRepository
    {
        private readonly MyDatabase _myDatabase;
        private readonly CartRepository _cartRepository;
        private readonly ProductRepository _productRepository;

        public OrderRepository(MyDatabase myDatabase, CartRepository cartRepository, ProductRepository productRepository)
        {
            _myDatabase = myDatabase;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public async Task<TemporaryOrder> TemporaryOrder ()
        {

            var temp = await _cartRepository.GetCart(1);
            var temporaryOrder = new TemporaryOrder
            {
                UserId = temp.UserId,
                Details =
                
            };
            
        }
    }
}
