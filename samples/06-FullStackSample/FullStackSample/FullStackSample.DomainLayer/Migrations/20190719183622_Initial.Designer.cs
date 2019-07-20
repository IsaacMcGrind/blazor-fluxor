﻿// <auto-generated />
using System;
using FullStackSample.DomainLayer.ServicesImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FullStackSample.DomainLayer.Migrations
{
    [DbContext(typeof(FullStackDbContext))]
    [Migration("20190719183622_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("uidx_Client_Name")
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("uidx_Product_Name")
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("TypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("uidx_ProductType_Name")
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.PurchaseOrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId");

                    b.Property<decimal>("Price");

                    b.Property<int?>("ProductId");

                    b.Property<long>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("PurchaseOrderLine");
                });

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.Product", b =>
                {
                    b.HasOne("FullStackSample.DomainLayer.Entities.ProductType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.PurchaseOrder", b =>
                {
                    b.HasOne("FullStackSample.DomainLayer.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("FullStackSample.DomainLayer.Entities.PurchaseOrderLine", b =>
                {
                    b.HasOne("FullStackSample.DomainLayer.Entities.PurchaseOrder", "Order")
                        .WithMany("Lines")
                        .HasForeignKey("OrderId");

                    b.HasOne("FullStackSample.DomainLayer.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
