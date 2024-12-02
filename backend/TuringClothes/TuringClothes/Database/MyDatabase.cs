using Microsoft.EntityFrameworkCore;

namespace TuringClothes.Database
{
    public class MyDatabase : DbContext
    {
        private const string DATABASE_PATH = "turing.db";

        //Tablas o entidades de la DB
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<TemporaryOrder> TemporaryOrders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }


        //#if DEBUG
        //#else
        //#endif

        //Configura EF para crear un archivo de la base de datos Sqlite

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
#if DEBUG

            optionsBuilder.UseSqlite($"Datasource={basedir}{DATABASE_PATH}");
#else
            optionsBuilder.UseMySql($"Datasource={basedir}{DATABASE_PATH}");
#endif
        }
    }
}
