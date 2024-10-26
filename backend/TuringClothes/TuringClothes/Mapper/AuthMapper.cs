using TuringClothes.Database;
using TuringClothes.Model;

namespace TuringClothes.Mapper
{
    public class AuthMapper
    {
        public AuthDto ToDto(User user)
        {
            return new AuthDto
            {
                Email = user.Email,
                Password = user.Password,
            };
        }

       
    }
}
