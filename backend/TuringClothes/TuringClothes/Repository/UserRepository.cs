using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        
        
    }
}
