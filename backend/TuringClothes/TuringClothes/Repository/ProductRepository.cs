using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public class ProductRepository 
    {
        private readonly MyDatabase _myDataBase;
        public ProductRepository(MyDatabase myDatabase) 
        {
            _myDataBase = myDatabase;
        }



        public async Task<Product?> GetProductById(int id)
        {
            return await _myDataBase.Products.Include(p => p.Reviews).FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
