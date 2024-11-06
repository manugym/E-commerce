using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

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

            Product[] products ={
                new Product
                {
                    Name = "Sudadera blanca cuello redondo",
                    Description = "Lo mejor contra el frio",
                    Price = 30,
                    Stock = 50,
                    Image = "images/Sudadera-cuello-redondo-clásica.png"
                },
                new Product
                {
                    Name = "Camiseta",
                    Description = "Una bonita camiseta blanca",
                    Price = 20,
                    Stock = 100,
                    Image = "images/camisetaBlanca.png"
                },
                new Product
                {
                    Name = "Vestido largo",
                    Description = "Un vestido largo Turing",
                    Price = 26,
                    Stock = 200,
                    Image = "images/Vestido-Largo.jpg"
                },
                new Product
                {
                    Name = "Cazadora marrón efecto ante",
                    Description = "Lo mejor contra el frio",
                    Price = 40,
                    Stock = 50,
                    Image = "images/cazadoraAnte.png"
                },
                new Product
                {
                    Name = "Polo gris hombre",
                    Description = "Lo mejor contra el frio",
                    Price = 20,
                    Stock = 50,
                    Image = "images/Polo-hombre.png"
                },
                new Product
                {
                    Name = "Camiseta negra con cuello redondo",
                    Description = "Lo mejor contra el frio",
                    Price = 20,
                    Stock = 50,
                    Image = "images/Camiseta-cuello-redondo-black.png"
                },
                new Product
                {
                    Name = "Chaqueta de traje",
                    Description = "Lo mejor contra el frio",
                    Price = 35,
                    Stock = 30,
                    Image = "images/chaqueta-traje.png"
                },
                new Product
                {
                    Name = "Traje beige",
                    Description = "Lo mejor contra el frio",
                    Price = 80,
                    Stock = 30,
                    Image = "images/Traje-beige.png"
                },
                new Product
                {
                    Name = "Cazadora negra mujer",
                    Description = "Lo mejor contra el frio",
                    Price = 34,
                    Stock = 50,
                    Image = "images/cazadora-negra-mujer.png"
                },
                new Product
                {
                    Name = "Blazer efecto piel",
                    Description = "Lo mejor contra el frio",
                    Price = 35,
                    Stock = 50,
                    Image = "images/blazer-efecto-piel.png"
                },
                new Product
                {
                    Name = "Calcetines",
                    Description = "Lo mejor contra el frio",
                    Price = 6,
                    Stock = 40,
                    Image = "images/calcetines.png"
                },
                new Product
                {
                    Name = "Chaqueta vaquera",
                    Description = "Lo mejor contra el frio",
                    Price = 36,
                    Stock = 40,
                    Image = "images/chaqueta-vaquera.png"
                },
                new Product
                {
                    Name = "Sudadera cremallera cuello",
                    Description = "Lo mejor contra el frio",
                    Price = 35,
                    Stock = 60,
                    Image = "images/sudadera-cremallera-cuello.png"
                },
                new Product
                {
                    Name = "Jersey canalé blanco",
                    Description = "Lo mejor contra el frio",
                    Price = 33,
                    Stock = 25,
                    Image = "images/jersey-canale-blanco.png"
                },
                new Product
                {
                    Name = "Jersey canalé oscuro",
                    Description = "Lo mejor contra el frio",
                    Price = 33,
                    Stock = 25,
                    Image = "images/jersey-canale-oscuro.png"
                },
                new Product
                {
                    Name = "Pantalón wide fit oscuro",
                    Description = "Lo mejor contra el frio",
                    Price = 40,
                    Stock = 28,
                    Image = "images/pantalon-wide-fit-negro.png"
                },
                new Product
                {
                    Name = "Pantalón vaquero hombre",
                    Description = "Lo mejor contra el frio",
                    Price = 40,
                    Stock = 28,
                    Image = "images/pantalon-vaquero-hombre.png"
                },
                new Product
                {
                    Name = "Chaleco negro mujer",
                    Description = "Lo mejor contra el frio",
                    Price = 40,
                    Stock = 25,
                    Image = "images/chaleco-negro-mujer.png"
                },
                new Product
                {
                    Name = "Pantalón recto soft",
                    Description = "Lo mejor contra el frio",
                    Price = 40,
                    Stock = 25,
                    Image = "images/pantalon-recto-soft.png"
                },
                new Product
                {
                    Name = "Jersey punto cuello alto",
                    Description = "Lo mejor contra el frio",
                    Price = 40,
                    Stock = 25,
                    Image = "images/jersey-punto-cuello-alto.png"
                },
                new Product
                {
                    Name = "Camisa gris mujer",
                    Description = "Lo mejor contra el frio",
                    Price = 20,
                    Stock = 26,
                    Image = "images/Camisa-gris-mujer.png"
                },
                new Product
                {
                    Name = "Pantalón recto tiro medio",
                    Description = "Lo mejor contra el frio",
                    Price = 37,
                    Stock = 30,
                    Image = "images/Pantalon-recto-tiro-medio.png"
                },
                new Product
                {
                    Name = "Vestido negro cinturón",
                    Description = "Lo mejor contra el frio",
                    Price = 45,
                    Stock = 15,
                    Image = "images/vestido-negro-cinturon.png"
                },
                new Product
                {
                    Name = "Bomber negra acolchada",
                    Description = "Lo mejor contra el frio",
                    Price = 38,
                    Stock = 15,
                    Image = "images/bomber-negra-acolchada.png"
                },

            };
            _context.Users.AddRange(users);
            _context.Products.AddRange(products);
            _context.SaveChanges();
            Console.WriteLine("datos insertados en la db");
        }
    }

}
