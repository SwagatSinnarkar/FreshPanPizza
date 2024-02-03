using FreshPanPizza.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreshPanPizza.Repositories
{
    public class AppDBContext : IdentityDbContext<User, Role, int> 
    {
        //needed for the migration only.
        public AppDBContext()
        {

        }

        //Read the configuration from your settings.
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Payment> Payment { get; set; }                    

        //Define DB Connection String to do the Migration so that we can create the databases.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LS5LGMV;Initial Catalog=FreshPanPizzaDb; Integrated Security=true; Encrypt=Yes;TrustServerCertificate=Yes; ");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
