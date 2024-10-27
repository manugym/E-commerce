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
        public IEnumerable<AuthDto> ToDto(IEnumerable<User> users)
        {
            return users.Select(user => ToDto(user));
        }
    }
}