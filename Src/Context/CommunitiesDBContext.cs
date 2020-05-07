using Microsoft.EntityFrameworkCore;
using CommunitiesWinApi.Models;
namespace CommunitiesWinApi.Context
{
    public partial class CommunitiesDBContext : DbContext
    {
        public CommunitiesDBContext()
        {  }

        public CommunitiesDBContext(DbContextOptions<CommunitiesDBContext> options)
            : base(options)
        {  }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<VendorCategory> VendorCategory { get; set; }
        public virtual DbSet<VendorDetails> VendorDetails { get; set; }
        public virtual DbSet<VendorProduct> VendorProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=communitieswin;Integrated Security=True;User ID=sa;pwd=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<VendorCategory>(entity =>
            {
                entity.ToTable("vendor_category");

                entity.Property(e => e.VendorCategoryId).HasColumnName("vendor_category_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<VendorDetails>(entity =>
            {
                entity.HasKey(x => x.VendorId)
                    .HasName("PK__vendor_d__0F7D2B7888674A3F");

                entity.ToTable("vendor_details");

                entity.HasIndex(x => x.Phone)
                    .HasName("UQ__vendor_d__B43B145F6A7FE48D")
                    .IsUnique();

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(100);

                entity.Property(e => e.IsFeverScreen)
                    .HasColumnName("is_fever_screen")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsSanitizerUsed)
                    .HasColumnName("is_sanitizer_used")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsSocialDistance)
                    .HasColumnName("is_social_distance")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsStampCheck)
                    .HasColumnName("is_stamp_check")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(12, 9)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(12, 9)");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Pin)
                    .HasColumnName("pin")
                    .HasMaxLength(20);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(100);

                entity.Property(e => e.VendorName)
                    .IsRequired()
                    .HasColumnName("vendor_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VendorProduct>(entity =>
            {
                entity.ToTable("vendor_product");

                entity.Property(e => e.VendorProductId).HasColumnName("vendor_product_id");

                entity.Property(e => e.MinOrderQuantity)
                    .HasColumnName("min_order_quantity")
                    .HasColumnType("decimal(20, 0)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(20, 2)");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Units).HasMaxLength(20);

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
