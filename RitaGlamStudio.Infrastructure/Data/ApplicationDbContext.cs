using Microsoft.EntityFrameworkCore;
using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //1º modelo a ser criado
        public DbSet<Category> Categories { get; set; }

        //2º modelo a ser criado
        public DbSet<Brand> Brands { get; set; }

        //3º modelo a ser criado
        public DbSet<MakeupProduct> MakeupProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(

                 new Category
                 {
                     Id = 1,
                     Name = "Face",
                 },

                new Category
                {
                    Id = 2,
                    Name = "Eyes",
                },

                new Category
                {
                    Id = 3,
                    Name = "Lips",
                },

                new Category
                {
                    Id = 4,
                    Name = "Skincare",
                },

                new Category
                {
                    Id = 5,
                    Name = "Body",
                }
             );

            modelBuilder.Entity<Brand>().HasData(

                new Brand
                {
                    Id = 1,
                    Name = "MAC Cosmetics",
                },

               new Brand
               {
                   Id = 2,
                   Name = "Maybelline New York",
               },

               new Brand
               {
                   Id = 3,
                   Name = "L'Oréal Paris",
               },

               new Brand
               {
                   Id = 4,
                   Name = "NARS Cosmetics",
               },

               new Brand
               {
                   Id = 5,
                   Name = "Urban Decay",
               }
            );

            modelBuilder.Entity<MakeupProduct>().HasData(

                new MakeupProduct
                {
                    Id = 1,
                    Name = "MAC Ruby Woo Lipstick",
                    BrandId = 1,    //MAC Cosmetics
                    CategoryId = 3, // Lips
                    Price = 27,
                    Stock = 50,
                    Description = "A vibrant, matte, red lipstick.",
                    ImageUrl = "https://example.com/mac-ruby-woo.jpg"
                },
                new MakeupProduct
                {
                    Id = 2,
                    Name = "Maybelline Lash Sensational Mascara",
                    BrandId = 2,    //Maybelline New York
                    CategoryId = 2, // Eyes
                    Price = 12,
                    Stock = 75,
                    Description = "A lengthening and volumizing mascara.",
                    ImageUrl = "https://example.com/maybelline-lash-sensational.jpg"
                },
                new MakeupProduct
                {
                    Id = 3,
                    Name = "L'Oréal Paris True Match Foundation",
                    BrandId = 3,    //L'Oréal Paris
                    CategoryId = 1, // Face
                    Price = 15,
                    Stock = 130,
                    Description = "A blendable foundation that matches skin tone.",
                    ImageUrl = "https://example.com/loreal-true-match.jpg"
                },
                new MakeupProduct
                {
                    Id = 4,
                    Name = "NARS Radiant Creamy Concealer",
                    BrandId = 4,    //NARS Cosmetics
                    CategoryId = 1, //Face
                    Price = 30,
                    Stock = 190,
                    Description = "A creamy concealer with radiant finish.",
                    ImageUrl = "https://example.com/nars-creamy-concealer.jpg"
                },
                new MakeupProduct
                {
                    Id = 5,
                    Name = "Urban Decay Naked Eyeshadow Palette",
                    BrandId = 5,    //Urban Decay
                    CategoryId = 2, //Eyes
                    Price = 54,
                    Stock = 30,
                    Description = "A versatile eyeshadow palette with neutral shades.",
                    ImageUrl = "https://example.com/urban-decay-naked.jpg"
                }
            );
        }
    }
}
