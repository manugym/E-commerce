using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using TuringClothes.Database;

namespace TuringClothes.Services
{
    public class ReviewService
    {
        private readonly MyDatabase _myDatabase;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public ReviewService(MyDatabase context, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
            {
                _myDatabase = context;
                _predictionEnginePool = predictionEnginePool;
            }

            public async Task<double> GetAverageRatingForProduct(int productId)
            {
                var averageRating = await _myDatabase.Reviews
                    .Where(r => r.ProductId == productId)
                    .AverageAsync(r => r.Rating);
                return (averageRating + 1) * 2.5; //para que se podamos usarlo con estrellas
            }
            
            public async Task<int> GetRatingReview(string reviewText)
            {
                var input = new ModelInput { Text = reviewText };
                var result = _predictionEnginePool.Predict(input);

                return (int)Math.Round(result.PredictedLabel);
            }

        }

    }

