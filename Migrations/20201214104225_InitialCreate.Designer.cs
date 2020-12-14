﻿// <auto-generated />
using System;
using Assignment1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment1.Migrations
{
    [DbContext(typeof(SchoolCommunityContext))]
    [Migration("20201214104225_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Assignment1.Models.Advertisement", b =>
                {
                    b.Property<int>("AdvertisementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdvertisementId");

                    b.ToTable("Advertisement");
                });

            modelBuilder.Entity("Assignment1.Models.Community", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Community");
                });

            modelBuilder.Entity("Assignment1.Models.CommunityMembership", b =>
                {
                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<string>("CommunityID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.HasKey("StudentID", "CommunityID");

                    b.HasIndex("CommunityID");

                    b.ToTable("CommunityMembership");
                });

            modelBuilder.Entity("Assignment1.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Assignment1.Models.StudentMembership", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommunityID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CommunityID");

                    b.HasIndex("StudentID");

                    b.ToTable("StudentMembership");
                });

            modelBuilder.Entity("Assignment1.Models.CommunityMembership", b =>
                {
                    b.HasOne("Assignment1.Models.Community", "Community")
                        .WithMany("CommunityMemberships")
                        .HasForeignKey("CommunityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment1.Models.Student", "Student")
                        .WithMany("CommunityMemberships")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assignment1.Models.StudentMembership", b =>
                {
                    b.HasOne("Assignment1.Models.Community", "Community")
                        .WithMany()
                        .HasForeignKey("CommunityID");

                    b.HasOne("Assignment1.Models.Student", "Student")
                        .WithMany("StudentMemberships")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}