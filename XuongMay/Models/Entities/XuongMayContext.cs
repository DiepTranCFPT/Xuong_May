using Microsoft.EntityFrameworkCore;

namespace XuongMay.Models.Entity;

public class XuongMayContext : DbContext
{
    public XuongMayContext(DbContextOptions<XuongMayContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Chain> Chains { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite Key for OrderDetails
        modelBuilder.Entity<OrderDetails>()
             .HasKey(od => new { od.OrderId, od.ProductId });
        modelBuilder.Entity<Chain>()
             .HasOne(b => b.Account)
             .WithMany(a => a.Chains)
             .HasForeignKey(b => b.ManagerId);
        base.OnModelCreating(modelBuilder);
        // Other configurations can go here
    }
}