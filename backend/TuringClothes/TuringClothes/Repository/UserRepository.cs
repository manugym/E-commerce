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
        
        //public async Task<ActionResult> Register([FromBody] User registerData)
        //{
        //    var newUser = new User
        //    {
        //        Name = registerData.Name,
        //        Surname = registerData.Surname,
        //        Email = registerData.Email,
        //        Password = registerData.Password,
        //        Address = registerData.Address,
        //        Role = "User"
        //    };
        //    await _myDatabase.Users.AddAsync(newUser);
        //    await _myDatabase.SaveChangesAsync();
        //    //return Ok("Usuario registrado");
        
        //}
    }
}
