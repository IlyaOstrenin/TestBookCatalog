using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TestBookCatalog.Models;

namespace TestBookCatalog
{
    public class TestBookCatalogDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<MediaFile> MediaFiles { get; set; }
        public virtual DbSet<BookCategory> BooksCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Favorite> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };

            User adminUser = new User { Id = Guid.NewGuid(), RefreshToken = Guid.NewGuid(), PhoneNumber = "79000000000", RoleId = adminRole.Id };

            Category category1 = new Category { Id = 1, Name = "Детектив" };
            Category category2 = new Category { Id = 2, Name = "Наука" };
            Category category3 = new Category { Id = 3, Name = "Фантастика" };
            Category category4 = new Category { Id = 3, Name = "Роман" };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<Category>().HasData(new Category[] { category1, category2, category3, category4 });

            modelBuilder.Entity<BookCategory>().HasKey(c => new { c.BookId, c.CategoryId });
            modelBuilder.Entity<Favorite>().HasKey(c => new { c.BookId, c.UserId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile($"appsettings.Development.json");
            var config = builder.Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("Db"));
        }
    }
}
