﻿// <auto-generated />
using System;
using Afi.Registration.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Afi.Registration.Persistence.Migrations
{
    [DbContext(typeof(AfiDbContext))]
    partial class AfiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Afi.Registration.Domain.Models.Customer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Afi.Registration.Domain.Models.Policy", b =>
                {
                    b.Property<int>("PolicyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Key", 0);

                    b.Property<DateTime?>("ActivatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PolicyReference")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.HasKey("PolicyId");

                    b.HasAlternateKey("PolicyReference");

                    b.HasIndex("OwnerId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Afi.Registration.Domain.Models.Policy", b =>
                {
                    b.HasOne("Afi.Registration.Domain.Models.Customer", "Owner")
                        .WithMany("Policies")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Afi.Registration.Domain.Models.Customer", b =>
                {
                    b.Navigation("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}