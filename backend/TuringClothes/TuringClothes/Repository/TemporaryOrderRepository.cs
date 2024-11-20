using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;
using TuringClothes.Model;

namespace TuringClothes.Repository
{
    public class TemporaryOrderRepository
    {
        private readonly MyDatabase _myDatabase;
        private readonly ProductRepository _productRepository;


        public TemporaryOrderRepository(MyDatabase database, ProductRepository productRepository)
        {
            _myDatabase = database;
            _productRepository = productRepository;

        }

        public async Task AddTemporaryOrderAsync(long userId, ICollection<CartDetail> cartDetails)
        {
            var orderDetails = cartDetails.Select(cart => new OrderDetail
            {
                ProductID = cart.ProductId,
                Amount = cart.Amount,
                Product = cart.Product
            }).ToList();

            var temporaryOrder = new TemporaryOrder
            {
                UserId = userId,
                Details = orderDetails
            };

            /*
             * DESCONTAR EL STOCK DE LOS PRODUCTOS RESERVADOS
             * ANTES DE GUARDAR EN LA BASE DE DATOS
             */

            await _myDatabase.TemporaryOrders.AddAsync(temporaryOrder);
            await _myDatabase.SaveChangesAsync();
        }

    }
}
