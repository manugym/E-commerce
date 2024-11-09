using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;

namespace TuringClothes.Services
{
    public class ReviewService
    {
        private readonly MyDatabase _myDatabase;

        public ReviewService(MyDatabase context)
            {
                _myDatabase = context;
            }

            public async Task<double> GetAverageRatingForProduct(int productId)
            {
                return await _myDatabase.Reviews
                    .Where(r => r.ProductId == productId)
                    .AverageAsync(r => r.Rating);
            }
        }

    }

