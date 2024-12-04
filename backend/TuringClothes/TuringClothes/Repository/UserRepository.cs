using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;

namespace TuringClothes.Repository
{
    public class UserRepository
    {
        private readonly MyDatabase _myDatabase;
        public UserRepository(MyDatabase myDatabase)
        {
            _myDatabase = myDatabase;
        }

        public async Task<User> GetUserById(long userId)
        {
            return _myDatabase.Users.FirstOrDefault(u => u.Id == userId);
        }

        public async Task AddAsync(User user)
        {
            await _myDatabase.AddAsync(user);
        }


    }
}
