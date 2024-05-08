using Microsoft.EntityFrameworkCore;
using RitaGlamStudio.Domain.Entities;

namespace RitaGlamStudio.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }
        public DbSet<Category> Categories { get; set; }

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
        }
    }
}
