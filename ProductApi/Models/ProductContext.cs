using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models
{
    public class ProductContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<CartItem> Carts { get; set; }
        public DbSet<StockProduct> StockProducts { get; set; }
        public DbSet<CartItem> CartItem { get; set; } = default!;
        public DbSet<ShippingDetails> ShippingDetails { get; set; } = default!;
        public DbSet<PaymentDetails> PaymentDetails { get; set; } = default!;
        //added by anil 19th sep
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItems> OrderItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"server=.\sqlexpress;  initial catalog = jeanStation;integrated security = true; trustservercertificate = true");
                }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable(ot=>ot.HasTrigger("trgAddOrderItems"));

        }
    }
}
