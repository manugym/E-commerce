using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public class AuthRepository : Repository<User, int>
    {
        public AuthRepository(MyDatabase myDatabase) : base(myDatabase) 
        { 
        }

        public async Task <ICollection<User>> GetByEmail(string mail)
        {
            return await GetQueryable().Where(email => email.Email == mail).ToArrayAsync();
        }
    }
}
