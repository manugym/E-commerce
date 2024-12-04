using TuringClothes.Helpers;
using TuringClothes.Model;

namespace TuringClothes.Services
{
    public class ImageService
    {
        private const string IMAGES_FOLDER = "images";
        private readonly UnitOfWork _unitOfWork;

        public ImageService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> InsertImage(IFormFile file)
        {
            string path = $"{IMAGES_FOLDER}{Guid.NewGuid()}_{file.FileName}";
            await StoreImageAsync(path, file);
            return path;
        }

        private async Task StoreImageAsync(string path, IFormFile file)
        {
            using Stream stream = file.OpenReadStream();
            await FileHelper.SaveAsync(stream, path);
        }
    }
}
