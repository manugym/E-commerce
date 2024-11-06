using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Pagination;
using TuringClothes.Services;



namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;
        private readonly CatalogService _catalogService;
        
       
        public CatalogController(MyDatabase myDatabase, CatalogService catalogService) 
        { 
            _myDatabase = myDatabase;
            _catalogService = catalogService; 
            

        }

        [HttpGet("ObtenerProductos")]
        public IEnumerable<Product> GetProducts()
        {
            return _myDatabase.Products;
        }


        [AllowAnonymous]
        [HttpGet ("ProductosPaginados")]
        public  ActionResult<PagedResults<Product>> GetPagination([FromQuery] PaginationParams paginationQuery)
        {
            var results = _catalogService.GetPaginationCatalog(paginationQuery);
            return Ok(results);
        }

       
        

    }
}