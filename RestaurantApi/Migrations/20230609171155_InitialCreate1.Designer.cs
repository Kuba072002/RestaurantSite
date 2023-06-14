﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RestaurantApi.Data;

#nullable disable

namespace RestaurantApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230609171155_InitialCreate1")]
    partial class InitialCreate1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DishOrder", b =>
                {
                    b.Property<int>("DishesId")
                        .HasColumnType("integer");

                    b.Property<int>("OrdersId")
                        .HasColumnType("integer");

                    b.HasKey("DishesId", "OrdersId");

                    b.HasIndex("OrdersId");

                    b.ToTable("DishOrder");
                });

            modelBuilder.Entity("RestaurantApi.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FreshnessTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("RestaurantApi.Models.ComponentOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<string>("FreshnessDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<int?>("WholesalerOrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("WholesalerOrderId");

                    b.ToTable("ComponentOrders");
                });

            modelBuilder.Entity("RestaurantApi.Models.ComponentQunatity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<string>("FreshnessDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("ComponentQunatities");
                });

            modelBuilder.Entity("RestaurantApi.Models.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Created_at")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PictureId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PictureId")
                        .IsUnique();

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("RestaurantApi.Models.DishComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("DishId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("DishId");

                    b.ToTable("DishComponents");
                });

            modelBuilder.Entity("RestaurantApi.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("OrderDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RestaurantApi.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Size")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("RestaurantApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Created_at")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RestaurantApi.Models.Wholesaler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");
                });

            modelBuilder.Entity("RestaurantApi.Models.WholesalerOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("OrderDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WholesalerId");

                    b.ToTable("WholesalerOrders");
                });

            modelBuilder.Entity("DishOrder", b =>
                {
                    b.HasOne("RestaurantApi.Models.Dish", null)
                        .WithMany()
                        .HasForeignKey("DishesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantApi.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantApi.Models.ComponentOrder", b =>
                {
                    b.HasOne("RestaurantApi.Models.Component", "Component")
                        .WithMany("ComponentOrders")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantApi.Models.WholesalerOrder", null)
                        .WithMany("ComponentOrders")
                        .HasForeignKey("WholesalerOrderId");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("RestaurantApi.Models.ComponentQunatity", b =>
                {
                    b.HasOne("RestaurantApi.Models.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("RestaurantApi.Models.Dish", b =>
                {
                    b.HasOne("RestaurantApi.Models.Picture", "Picture")
                        .WithOne("Dish")
                        .HasForeignKey("RestaurantApi.Models.Dish", "PictureId");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("RestaurantApi.Models.DishComponent", b =>
                {
                    b.HasOne("RestaurantApi.Models.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantApi.Models.Dish", null)
                        .WithMany("DishComponents")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("RestaurantApi.Models.Order", b =>
                {
                    b.HasOne("RestaurantApi.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RestaurantApi.Models.WholesalerOrder", b =>
                {
                    b.HasOne("RestaurantApi.Models.Wholesaler", "Wholesaler")
                        .WithMany("WholesalerOrders")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("RestaurantApi.Models.Component", b =>
                {
                    b.Navigation("ComponentOrders");
                });

            modelBuilder.Entity("RestaurantApi.Models.Dish", b =>
                {
                    b.Navigation("DishComponents");
                });

            modelBuilder.Entity("RestaurantApi.Models.Picture", b =>
                {
                    b.Navigation("Dish")
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantApi.Models.User", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantApi.Models.Wholesaler", b =>
                {
                    b.Navigation("WholesalerOrders");
                });

            modelBuilder.Entity("RestaurantApi.Models.WholesalerOrder", b =>
                {
                    b.Navigation("ComponentOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
