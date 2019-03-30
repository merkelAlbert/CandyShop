﻿// <auto-generated />
using System;
using CandyShop.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CandyShop.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190321123345_OrderCreationDate")]
    partial class OrderCreationDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CandyShop.DAL.Models.IntermediateModels.OrderPastry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<Guid>("OrderId");

                    b.Property<Guid>("PastryId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PastryId");

                    b.ToTable("OrderPastry");
                });

            modelBuilder.Entity("CandyShop.DAL.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CandyShop.DAL.Models.Pastry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Compound");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("PastryType");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Pastries");
                });

            modelBuilder.Entity("CandyShop.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CandyShop.DAL.Models.IntermediateModels.OrderPastry", b =>
                {
                    b.HasOne("CandyShop.DAL.Models.Order", "Order")
                        .WithMany("Pastries")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CandyShop.DAL.Models.Pastry", "Pastry")
                        .WithMany("Orders")
                        .HasForeignKey("PastryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CandyShop.DAL.Models.Order", b =>
                {
                    b.HasOne("CandyShop.DAL.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}