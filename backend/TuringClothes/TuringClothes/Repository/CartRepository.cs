using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;


namespace TuringClothes.Repository
{
    public class CartRepository
    {
        private readonly MyDatabase myDatabase;
        public CartRepository(MyDatabase database)
        {
            myDatabase = database;

        }

        public async Task<ActionResult> AddToCar(Product id, User identity )
        {

        }
    }
}
