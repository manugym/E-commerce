using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartSearchController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Search([FromQuery] string query) 
        {
            SmartSearchService smartSearchService = new SmartSearchService();
            return smartSearchService.Search(query);
        }

    }
}
