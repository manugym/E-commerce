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

        public async Task<Cart> AddItemToCar(long productId, long userId)
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
            var existingDetails = cart.Details.FirstOrDefault(d => d.ProductId == productId);

            if (existingDetails != null)
            {
                existingDetails.Amount += 1;
            }
            else
            {
                var cartDetails = new CartDetail
                {
                    ProductId = productId,
                    Amount = 1
                };
                cart.Details.Add(cartDetails);
            }

            await _myDatabase.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> RemoveItemFromCart(long productId, long userId)
        {
            var cart = await GetCart(userId);

            var cartDetails = cart.Details.FirstOrDefault(d => d.ProductId == productId);
            if (cartDetails != null)
            {
                cartDetails.Amount -= 1;

                if (cartDetails.Amount == 0)
                {
                    cart.Details.Remove(cartDetails);
                }
            }

            await _myDatabase.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> UpdateItemInCart(long productId, int amount, long userId)
        {
            var cart = await GetCart(userId);

            if (cart == null)
            {
                return false;
            }

            var cartDetails = cart.Details.FirstOrDefault(d => d.ProductId == productId);

            if (cartDetails == null)
            {
                if(amount > 0)
                {
                    var newDetails = new CartDetail
                    {
                        Amount = amount,
                        ProductId = productId,
                    };
                    cart.Details.Add(newDetails);
                }          
            } 
            else
            {
                
                if (amount == 0)
                {
                    cart.Details.Remove(cartDetails);
                }
                else if (cartDetails.Amount != amount)
                {
                    cartDetails.Amount = amount;
                }

            }

            await _myDatabase.SaveChangesAsync();

            return true;

        }
    }
}