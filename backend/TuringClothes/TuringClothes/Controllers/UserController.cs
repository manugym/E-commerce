using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Model;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UserController(UnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetUserOrders()
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            var orders = await _unitOfWork.UserRepository.GetordersByUser(userIdLong);
            return Ok(orders);
        }
    }
}


