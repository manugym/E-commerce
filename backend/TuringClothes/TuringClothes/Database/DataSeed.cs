namespace TuringClothes.Database
{
    public class DataSeed
    {
        private readonly MyDatabase _context;
        public DataSeed(MyDatabase context)
        {
            _context = context;
        }

        public void Seed()
        {
            User[] users =
            {
                new User
            {
                Name = "Juanma",
                Surname = "López Navarro",
                Email = "juanmita@gmail.com",
                Password = "123456",
<<<<<<< Updated upstream
                Address = "Calle La Fuente 3"
=======
                Address = "Calle La Fuente 3",
                Role = "admin"
>>>>>>> Stashed changes
            },
            new User
            {
                Name = "Francisco",
                Surname = "Ruiz Sánchez",
                Email = "franruiz@gmail.com",
                Password = "12345678",
                Address = "Calle Hermes 4"
            },
            new User
            {
                Name = "Daniel",
                Surname = "Díaz Pérez",
                Email = "danidiazperez@gmail.com",
                Password = "1234567",
                Address = "Calle Hermes 4"
            },
            new User
            {
                Name = "Nerea",
                Surname = "Muñoz Pérez",
                Email = "nereamuñoz@hotmail.com",
                Password = "12345",
                Address = "Calle Hermes 4"
            },
            new User
            {
                Name = "Julia",
                Surname = "Pérez Cabrera",
                Email = "julitaperez@gmail.com",
                Password = "123456",
                Address = "Calle Navarro Ledesma 13"
            },
            new User
            {
                Name = "Noe",
                Surname = "Frances",
                Email = "noe@gmail.com",
                    Password = "938382",
                Address = "Riogordo n0 3"
            }
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();
            Console.WriteLine("datos insertados en la db");
        }
    }

}
