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

        public async Task<IEnumerable<TemporaryOrder>> GetAllTemporaryOrdersAsync()
        {
            return await _myDatabase.TemporaryOrders
                .Include(order => order.Details)
                .ToListAsync();
        }

        public async Task AddTemporaryOrderAsync(ICollection<OrderDetail> orderDetails)
        //public async Task AddTemporaryOrderAsync(long userId, ICollection<CartDetail> cartDetails)
        {

            var temporaryOrder = new TemporaryOrder
            {
                //UserId = 
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
