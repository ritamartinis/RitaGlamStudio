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
        }
    }
}
