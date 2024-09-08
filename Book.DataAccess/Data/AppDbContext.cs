using Book.Models;
using Book.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Category>().HasData(
			new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
			new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
			new Category { Id = 3, Name = "History", DisplayOrder = 3 }
			 );

		
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "O'tgan Kunlar",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryID = 1,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryID = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryID = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryID = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryID = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryID = 3,
                    ImageUrl = ""
                }
            );

    }
}
