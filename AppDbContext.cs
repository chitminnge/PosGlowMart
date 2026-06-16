using GlowMart.Models;
using Microsoft.EntityFrameworkCore;

namespace GlowMart
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Store> Stores { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<CashShift> CashShifts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<StoreStock> StoreStocks { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Store
            modelBuilder.Entity<Store>().HasKey(s => s.StoreId);

            // Staff
            modelBuilder.Entity<Staff>().HasKey(s => s.StaffId);
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Store)
                .WithMany(st => st.Staffs)
                .HasForeignKey(s => s.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            // CashShift
            modelBuilder.Entity<CashShift>().HasKey(c => c.ShiftId);
            modelBuilder.Entity<CashShift>()
                .HasOne(c => c.Store)
                .WithMany(st => st.CashShifts)
                .HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CashShift>()
                .HasOne(c => c.Staff)
                .WithMany(st => st.CashShifts)
                .HasForeignKey(c => c.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Category
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);

            // Product
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductVariant
            modelBuilder.Entity<ProductVariant>().HasKey(v => v.VariantId);
            modelBuilder.Entity<ProductVariant>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.CostPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.SalePrice)
                .HasColumnType("decimal(18,2)");

            // StoreStock
            modelBuilder.Entity<StoreStock>().HasKey(ss => ss.StockId);
            modelBuilder.Entity<StoreStock>()
                .HasOne(ss => ss.Store)
                .WithMany(st => st.StoreStocks)
                .HasForeignKey(ss => ss.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StoreStock>()
                .HasOne(ss => ss.Variant)
                .WithMany(v => v.StoreStocks)
                .HasForeignKey(ss => ss.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sale
            modelBuilder.Entity<Sale>().HasKey(s => s.SaleId);
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Store)
                .WithMany(st => st.Sales)
                .HasForeignKey(s => s.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Staff)
                .WithMany(st => st.Sales)
                .HasForeignKey(s => s.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.CashShift)
                .WithMany(cs => cs.Sales)
                .HasForeignKey(s => s.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .Property(s => s.DiscountAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .Property(s => s.FinalAmount)
                .HasColumnType("decimal(18,2)");

            // SaleItem
            modelBuilder.Entity<SaleItem>().HasKey(si => si.SaleItemId);
            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.StoreStock)
                .WithMany(ss => ss.SaleItems)
                .HasForeignKey(si => si.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.SalePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.TotalPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
