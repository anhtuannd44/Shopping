using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Data.Interface;
using Microsoft.EntityFrameworkCore;
using ShoppingProject.Domain.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShoppingProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public virtual DbSet<PostCategory> PostCategories { set; get; }
        public virtual DbSet<Post> Posts { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Province> Province { set; get; }
        public virtual DbSet<District> District { set; get; }
        public virtual DbSet<Ward> Ward { set; get; }
        public virtual DbSet<Question> Question { set; get; }
        public virtual DbSet<Setting> Setting { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new WardConfiguration());

        }
    }
}
