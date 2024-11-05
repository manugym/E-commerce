using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;


        public CatalogController(MyDatabase myDatabase, ProductoService productoService)
        {
            _myDatabase = myDatabase;
        }

        [HttpGet("ObtenerProductos")]
        public IEnumerable<Product> GetProducts()
        {
            return _myDatabase.Products;
        }

       

    }
}