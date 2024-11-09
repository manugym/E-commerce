using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Pagination;
using TuringClothes.Repository;
using TuringClothes.Services;



namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly MyDatabase _myDatabase;
        private readonly CatalogService _catalogService;
        private readonly ProductRepository _productRepository;
        
       
        public CatalogController(MyDatabase myDatabase, CatalogService catalogService, ProductRepository productRepository) 
        { 
            _myDatabase = myDatabase;
            _catalogService = catalogService; 
            _productRepository = productRepository;

        }



        [HttpGet("GetProduct")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var result = await _productRepository.GetProductById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
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