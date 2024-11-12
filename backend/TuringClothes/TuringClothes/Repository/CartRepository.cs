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

        
    }
}
