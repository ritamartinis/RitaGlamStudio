﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RitaGlamStudio.Infrastructure.Data;

#nullable disable

namespace RitaGlamStudio.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RitaGlamStudio.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "MAC Cosmetics"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Maybelline New York"
                        },
                        new
                        {
                            Id = 3,
                            Name = "L'Oréal Paris"
                        },
                        new
                        {
                            Id = 4,
                            Name = "NARS Cosmetics"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Urban Decay"
                        });
                });

            modelBuilder.Entity("RitaGlamStudio.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Face"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Eyes"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lips"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Skincare"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Body"
                        });
                });

            modelBuilder.Entity("RitaGlamStudio.Domain.Entities.MakeupProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("MakeupProducts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            CategoryId = 3,
                            Description = "A vibrant, matte, red lipstick.",
                            ImageUrl = "https://example.com/mac-ruby-woo.jpg",
                            Name = "MAC Ruby Woo Lipstick",
                            Price = 27,
                            Stock = 50
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 2,
                            CategoryId = 2,
                            Description = "A lengthening and volumizing mascara.",
                            ImageUrl = "https://example.com/maybelline-lash-sensational.jpg",
                            Name = "Maybelline Lash Sensational Mascara",
                            Price = 12,
                            Stock = 75
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 3,
                            CategoryId = 1,
                            Description = "A blendable foundation that matches skin tone.",
                            ImageUrl = "https://example.com/loreal-true-match.jpg",
                            Name = "L'Oréal Paris True Match Foundation",
                            Price = 15,
                            Stock = 130
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 4,
                            CategoryId = 1,
                            Description = "A creamy concealer with radiant finish.",
                            ImageUrl = "https://example.com/nars-creamy-concealer.jpg",
                            Name = "NARS Radiant Creamy Concealer",
                            Price = 30,
                            Stock = 190
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 5,
                            CategoryId = 2,
                            Description = "A versatile eyeshadow palette with neutral shades.",
                            ImageUrl = "https://example.com/urban-decay-naked.jpg",
                            Name = "Urban Decay Naked Eyeshadow Palette",
                            Price = 54,
                            Stock = 30
                        });
                });

            modelBuilder.Entity("RitaGlamStudio.Domain.Entities.MakeupReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MakeupProductId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MakeupProductId");

                    b.ToTable("MakeupReviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientName = "Elisabeth Smith",
                            MakeupProductId = 1,
                            Rating = 5,
                            Review = "This lipstick is my all time favorite.",
                            ReviewDate = new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            ClientName = "Joana Higgins",
                            MakeupProductId = 2,
                            Rating = 4,
                            Review = "This mascara is great, but it smudges a little.",
                            ReviewDate = new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            ClientName = "Jane Roe",
                            MakeupProductId = 3,
                            Rating = 5,
                            Review = "The foundation has a perfect match for my skin tone.",
                            ReviewDate = new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            ClientName = "Alice Johnson",
                            MakeupProductId = 4,
                            Rating = 3,
                            Review = "The concealer works well but is a bit pricey.",
                            ReviewDate = new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            ClientName = "Bob Brown",
                            MakeupProductId = 5,
                            Rating = 5,
                            Review = "The eyeshadow palette has amazing colors and great pigmentation.",
                            ReviewDate = new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RitaGlamStudio.Domain.Entities.MakeupProduct", b =>
                {
                    b.HasOne("RitaGlamStudio.Domain.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RitaGlamStudio.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RitaGlamStudio.Domain.Entities.MakeupReview", b =>
                {
                    b.HasOne("RitaGlamStudio.Domain.Entities.MakeupProduct", "MakeupProduct")
                        .WithMany()
                        .HasForeignKey("MakeupProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MakeupProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
