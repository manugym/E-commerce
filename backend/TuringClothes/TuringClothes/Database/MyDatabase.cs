using Microsoft.EntityFrameworkCore;

namespace TuringClothes.Database
{
    public class MyDatabase : DbContext
    {
        private const string DATABASE_PATH = "turing.db";

        //Tablas o entidades de la DB
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }

        //Configura EF para crear un archivo de la base de datos Sqlite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            optionsBuilder.UseSqlite($"Datasource={basedir}{DATABASE_PATH}");
            /*optionsBuilder.LogTo(Console.WriteLine);*/
        }
    }
}
