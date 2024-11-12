using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;


namespace TuringClothes.Repository
{
    public class CartRepository
    {
        private readonly MyDatabase _myDatabase;
        public CartRepository(MyDatabase database)
        {
            _myDatabase = database;

        }

        public async Task<Cart> GetCart(long id) 
        {
            var cart = await _myDatabase.Carts.Include(d => d.Details).FirstOrDefaultAsync(u => u.UserId == id);

            return cart;
        }

        public async Task<Cart> AddItemToCar (long productId, long userId)
        {
            var cart = await GetCart(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Details = new List<CartDetail>()
                };
                _myDatabase.Carts.Add(cart);
            }
           var existingDetails = cart.Details.FirstOrDefault(d=> d.ProductId == productId);

            if (existingDetails != null) 
            {
                existingDetails.Amount += 1;
            }
            else { 
           var cartDetails = new CartDetail
            {
                ProductId = productId,
                Amount = 1
            };
                cart.Details.Add(cartDetails);
            }
            
           _myDatabase.SaveChanges();
            
           return cart;
        }
    }
}
