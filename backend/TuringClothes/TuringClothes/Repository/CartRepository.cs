using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Controllers;
using TuringClothes.Database;


namespace TuringClothes.Repository
{
    public class CartRepository
    {
        private readonly MyDatabase _myDatabase;
        private readonly ProductRepository _productRepository;
        public CartRepository(MyDatabase database, ProductRepository productRepository)
        {
            _myDatabase = database;
            _productRepository = productRepository;

        }

        public async Task<Cart> GetCart(long id)
        {
            var cart = await _myDatabase.Carts.Include(d => d.Details).ThenInclude(p => p.Product).FirstOrDefaultAsync(u => u.UserId == id);

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
                cart.Details.Remove(cartDetails);


                //cartDetails.Amount -= 1; // No puede ser esto así porque es duplicación de código. Esto ya se hace en el update.

                //if (cartDetails.Amount == 0)
                //{
                //    cart.Details.Remove(cartDetails);
                //}
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
            Product product = await _productRepository.GetProductById(productId);
            var cartDetails = cart.Details.FirstOrDefault(d => d.ProductId == productId);

            if (cartDetails == null)
            {
                if (amount > 0)
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

                /*
                 * ESTO LO HE TENIDO QUE MODIFICAR PARA PODER CONTROLAR QUE NO TE VACILEN CON EL STOCK
                 */

                switch (amount)
                {
                    case 0:

                        //cart.Details.Remove(cartDetails); // Para borrar vamos a usar el método remove
                        cartDetails.Amount = 1;
                        break;

                    case > 0 when amount > product.Stock:

                        cartDetails.Amount = (int)product.Stock;
                        break;

                    default:

                        if (cartDetails.Amount != amount)
                        {
                            cartDetails.Amount = amount;
                        }
                        break;
                }
            }

            await _myDatabase.SaveChangesAsync();

            return true;

        }
    }
}