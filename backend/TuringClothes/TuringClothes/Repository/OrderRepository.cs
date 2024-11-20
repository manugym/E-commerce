using TuringClothes.Database;
using TuringClothes.Model;

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

        public async Task<TemporaryOrder> TemporaryOrder(ICollection<OrderDetailDto> orderDetailDto)
        {
            using var transaction = await _myDatabase.Database.BeginTransactionAsync();
            try
            {
                long userId = 1;// cambiar luego con Authorize

                var temporaryOrder = new TemporaryOrder
                {
                    UserId = userId,
                    Details = new List<OrderDetail>()
                };

                _myDatabase.TemporaryOrders.Add(temporaryOrder);

                var noStockProducts = new List<OrderDetailDto>();

                foreach (var orderDetail in orderDetailDto)
                {
                    var product = await _productRepository.GetProductByIdOrder(orderDetail.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"El producto con la ID {orderDetail.ProductId} no existe.");
                    }

                    if (product.Stock >= orderDetail.Amount)
                    {
                        var newOrderDetail = new OrderDetail
                        {
                            ProductID = product.Id,
                            Amount = orderDetail.Amount,
                            TemporaryOrderID = temporaryOrder.Id,
                            Product = product
                        };

                        temporaryOrder.Details.Add(newOrderDetail);

                        product.Stock -= orderDetail.Amount;
                        _myDatabase.Products.Update(product);
                    }
                    else
                    {
                        noStockProducts.Add(orderDetail);
                    }


                }

                if (noStockProducts.Any())
                {
                    _myDatabase.Remove(temporaryOrder);
                    await _myDatabase.SaveChangesAsync();

                    var productNames = string.Join(", ", noStockProducts.Select(x => x.ProductId));
                    throw new Exception($"Los siguientes productos no tienen stock: {productNames}");

                }

                await _myDatabase.SaveChangesAsync();
                await transaction.CommitAsync();
                return temporaryOrder;
            }
            catch { await transaction.RollbackAsync(); 
                    throw; }
        }
    }
}
