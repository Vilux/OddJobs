﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebSecurityAssignment.Data;

namespace WebSecurityAssignment.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Address", b =>
                {
                    b.Property<int>("addressID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("city");

                    b.Property<string>("postalCode");

                    b.Property<string>("province");

                    b.Property<string>("streetAddress");

                    b.HasKey("addressID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Application", b =>
                {
                    b.Property<string>("ApplicantID");

                    b.Property<int>("JobID");

                    b.Property<string>("Comment");

                    b.HasKey("ApplicantID", "JobID");

                    b.HasIndex("JobID");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("ContactInfo");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Job", b =>
                {
                    b.Property<int>("jobID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("addressID");

                    b.Property<float>("amount");

                    b.Property<DateTime>("dateExpired");

                    b.Property<DateTime>("dateNeeded");

                    b.Property<string>("description");

                    b.Property<string>("employeeID");

                    b.Property<string>("employerID");

                    b.Property<string>("title");

                    b.HasKey("jobID");

                    b.HasIndex("addressID");

                    b.HasIndex("employerID");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Ratings", b =>
                {
                    b.Property<string>("employeeID");

                    b.Property<int>("jobID");

                    b.Property<string>("review");

                    b.Property<float>("score");

                    b.HasKey("employeeID", "jobID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Transaction", b =>
                {
                    b.Property<int>("transactionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("amount");

                    b.Property<string>("currency");

                    b.Property<DateTime>("date");

                    b.Property<string>("employeeID");

                    b.Property<string>("intent");

                    b.Property<int>("jobID");

                    b.Property<string>("paymentMethod");

                    b.Property<string>("paymentState");

                    b.Property<float>("paymentToEmployee");

                    b.Property<float>("paymentToProvider");

                    b.HasKey("transactionID");

                    b.HasIndex("employeeID");

                    b.HasIndex("jobID")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("WebSecurityAssignment.ViewModels.ApplicationVM", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicantID");

                    b.Property<string>("EmployeeFN");

                    b.Property<string>("EmployeeLN");

                    b.Property<string>("EmployerFN");

                    b.Property<string>("EmployerLN");

                    b.Property<int>("JobID");

                    b.Property<string>("JobTitle");

                    b.Property<string>("comments");

                    b.HasKey("ID");

                    b.ToTable("ApplicationVM");
                });

            modelBuilder.Entity("WebSecurityAssignment.ViewModels.RoleVM", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("RoleVM");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Application", b =>
                {
                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebSecurityAssignment.Data.Job", "Job")
                        .WithMany("Applications")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Job", b =>
                {
                    b.HasOne("WebSecurityAssignment.Data.Address", "Address")
                        .WithMany("Jobs")
                        .HasForeignKey("addressID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("Jobs")
                        .HasForeignKey("employerID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Ratings", b =>
                {
                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("Ratings")
                        .HasForeignKey("employeeID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebSecurityAssignment.Data.Transaction", b =>
                {
                    b.HasOne("WebSecurityAssignment.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("Transactions")
                        .HasForeignKey("employeeID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebSecurityAssignment.Data.Job", "Job")
                        .WithOne("Transaction")
                        .HasForeignKey("WebSecurityAssignment.Data.Transaction", "jobID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
