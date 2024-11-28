
using Stripe.Checkout;
using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public class OrderRepository
    {
        private readonly MyDatabase _myDatabase;
        private readonly TemporaryOrderRepository _temporaryOrderRepository;

        public OrderRepository(MyDatabase myDatabase, TemporaryOrderRepository temporaryOrderRepository)
        {
            _myDatabase = myDatabase;
            _temporaryOrderRepository = temporaryOrderRepository;
        }

        public async Task<Order> CreateOrder(long id, string paymentMethod, string status, long total)

        {
            var temporaryOrder = await _temporaryOrderRepository.GetTemporaryOrder(id);

            if (temporaryOrder == null)
            {
                throw new Exception($"La orden temporal {id} no existe.");
            }

            var order = new Order
            {
                UserId = temporaryOrder.UserId,
                PaymentMethod = paymentMethod,
                TransactionStatus = status,
                TotalPrice = total,
                OrderDetails = new List<OrderDetail>()
            };

            _myDatabase.Orders.Add(order);

            foreach (var temporaryOrderDetail in temporaryOrder.Details)
            {
                var newOrderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    Product = temporaryOrderDetail.Product,
                    ProductId = temporaryOrderDetail.ProductID,
                    Amount = temporaryOrderDetail.Amount,
                    ProductPrice = temporaryOrderDetail.Product.Price,
                };
                order.OrderDetails.Add(newOrderDetail);
            }
            await _myDatabase.SaveChangesAsync();
            return order;
        }
    }
}
