using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TuringClothes.Database;
using TuringClothes.Model;
using TuringClothes.Repository;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly MyDatabase _context;


        public ReviewController(MyDatabase context)
        {
            _context = context;

        }


        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByProduct(int productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .Select(r => new ReviewDto
                {
                    ProductId = r.ProductId,
                    Texto = r.Texto
                }).ToListAsync();

            return Ok(reviews);
        }

        
        [HttpPost]
        public async Task<ActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            var review = new Review
            {
                ProductId = reviewDto.ProductId,
                Texto = reviewDto.Texto
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewsByProduct), new { productId = review.ProductId }, reviewDto);
        }
    }

}