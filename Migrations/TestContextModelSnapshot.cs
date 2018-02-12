﻿// <auto-generated />
using coreDemo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace coreDemo.Migrations
{
    [DbContext(typeof(TestContext))]
    partial class TestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("coreDemo.Entity.ClassM", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ClassM");
                });

            modelBuilder.Entity("coreDemo.Entity.ClubM", b =>
                {
                    b.Property<int>("Sn")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Sn");

                    b.ToTable("ClubM");
                });

            modelBuilder.Entity("coreDemo.Entity.StudClub", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("ClubId");

                    b.HasKey("StudentId", "ClubId");

                    b.HasIndex("ClubId");

                    b.ToTable("StudClub");
                });

            modelBuilder.Entity("coreDemo.Entity.Student", b =>
                {
                    b.Property<int>("Sn")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("ClassId")
                        .IsRequired();

                    b.Property<int>("Hight");

                    b.Property<string>("Memo");

                    b.Property<string>("Name");

                    b.Property<string>("Photo");

                    b.Property<string>("SingleClassId");

                    b.Property<double>("Weight");

                    b.HasKey("Sn");

                    b.HasIndex("SingleClassId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("coreDemo.Entity.StudClub", b =>
                {
                    b.HasOne("coreDemo.Entity.ClubM", "Club")
                        .WithMany("StudClubs")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("coreDemo.Entity.Student", "Student")
                        .WithMany("StudClubs")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("coreDemo.Entity.Student", b =>
                {
                    b.HasOne("coreDemo.Entity.ClassM", "SingleClass")
                        .WithMany("Students")
                        .HasForeignKey("SingleClassId");
                });
#pragma warning restore 612, 618
        }
    }
}
