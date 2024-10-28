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
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("1234");
            var hashedPassword2 = BCrypt.Net.BCrypt.HashPassword("12345");
            var hashedPassword3 = BCrypt.Net.BCrypt.HashPassword("123456");
            var hashedPassword4 = BCrypt.Net.BCrypt.HashPassword("1234567");
            var hashedPassword5 = BCrypt.Net.BCrypt.HashPassword("1234321");
            var hashedPassword6 = BCrypt.Net.BCrypt.HashPassword("12342");

            User[] users =
            {
                new User
            {
                Name = "Juanma",
                Surname = "López Navarro",
                Email = "juanmita@gmail.com",
                Password = hashedPassword,
                Address = "Calle La Fuente 3",
                Role = "admin"
            },
            new User
            {
                Name = "Francisco",
                Surname = "Ruiz Sánchez",
                Email = "franruiz@gmail.com",
                Password = hashedPassword2,
                Address = "Calle Hermes 4",
                Role = "Alumno"
            },
            new User
            {
                Name = "Daniel",
                Surname = "Díaz Pérez",
                Email = "danidiazperez@gmail.com",
                Password = hashedPassword3,
                Address = "Calle Hermes 4",
                Role = "Alumno"
            },
            new User
            {
                Name = "Nerea",
                Surname = "Muñoz Pérez",
                Email = "nereamuñoz@hotmail.com",
                Password = hashedPassword4,
                Address = "Calle Hermes 4",
                Role = "Alumno"
            },
            new User
            {
                Name = "Julia",
                Surname = "Pérez Cabrera",
                Email = "julitaperez@gmail.com",
                Password = hashedPassword5,
                Address = "Calle Navarro Ledesma 13",
                Role = "Alumno"
            },
            new User
            {
                Name = "Noe",
                Surname = "Frances",
                Email = "noe@gmail.com",
                Password = hashedPassword6,
                Address = "Riogordo n0 3",
                Role = "Alumno"
            }
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();
            Console.WriteLine("datos insertados en la db");
        }
    }

}
