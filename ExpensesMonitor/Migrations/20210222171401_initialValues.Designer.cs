﻿// <auto-generated />
using System;
using ExpensesMonitor.SharedLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpensesMonitor.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20210222171401_initialValues")]
    partial class initialValues
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpensesMonitor.SharedLibrary.Models.CategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Bills"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Fast foods"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Shopping"
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Entertainment"
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Alcohol"
                        },
                        new
                        {
                            Id = 6,
                            CategoryName = "Fuel"
                        },
                        new
                        {
                            Id = 7,
                            CategoryName = "Others"
                        });
                });

            modelBuilder.Entity("ExpensesMonitor.SharedLibrary.Models.ExpenseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ExpensesMonitor.SharedLibrary.Models.ExpenseModel", b =>
                {
                    b.HasOne("ExpensesMonitor.SharedLibrary.Models.CategoryModel", "Category")
                        .WithMany("Expenses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}