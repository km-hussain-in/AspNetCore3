﻿// <auto-generated />
using System;
using DemoApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DemoApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("DemoApp.Data.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Frequency")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Recent")
                        .HasColumnType("TEXT");

                    b.Property<string>("Spot")
                        .HasColumnType("TEXT");

                    b.Property<string>("VisitorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VisitorId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("DemoApp.Data.Visitor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("DemoApp.Data.Visit", b =>
                {
                    b.HasOne("DemoApp.Data.Visitor", "Visitor")
                        .WithMany("Visits")
                        .HasForeignKey("VisitorId");
                });
#pragma warning restore 612, 618
        }
    }
}
