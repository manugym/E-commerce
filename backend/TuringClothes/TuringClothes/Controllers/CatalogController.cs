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
        private readonly ProductoService _productoService;


        public CatalogController(MyDatabase myDatabase, ProductoService productoService)
        {
            _myDatabase = myDatabase;
            _productoService = productoService;
        }

        [HttpGet("ObtenerProductos")]
        public IEnumerable<Product> GetProducts()
        {
            return _myDatabase.Products;
        }

            
        [HttpGet("filtrar")]
        public async Task<IActionResult> FiltrarProductos([FromQuery] ProductFilterDto filtros)
        {
        var productos = await _productoService.FiltrarProductosAsync(filtros);
        return Ok(productos);
        }
        


    }
}