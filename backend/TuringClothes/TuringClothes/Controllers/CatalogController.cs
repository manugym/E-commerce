using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;
        public CatalogController(MyDatabase myDatabase) 
        { 
            _myDatabase = myDatabase;
        }

        [HttpGet ("ObtenerProductos")]
        public IEnumerable<Product> GetProducts()
        {
            return _myDatabase.Products;
        }
    }
}
