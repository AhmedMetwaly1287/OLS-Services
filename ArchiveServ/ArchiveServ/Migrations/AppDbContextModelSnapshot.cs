﻿// <auto-generated />
using System;
using ArchiveService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArchiveServ.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ArchiveService.Models.Archive", b =>
                {
                    b.Property<int>("ArchiveID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<int>("BorrowedBookRequestID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ArchiveID");

                    b.ToTable("Archives");
                });
#pragma warning restore 612, 618
        }
    }
}
