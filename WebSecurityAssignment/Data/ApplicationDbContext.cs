using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSecurityAssignment.ViewModels;

namespace WebSecurityAssignment.Data
{
    public class Address
    {
        [Key]
        public int addressID { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public virtual ICollection<Job> Jobs
        { get; set; }
    }

    public class Job
    {
        [Key]
        public int jobID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string employerID { get; set; }
        public string employeeID { get; set; }
        public float amount { get; set; }
        public DateTime dateNeeded { get; set; }
        public DateTime dateExpired { get; set; }
        public int addressID { get; set; }
        public virtual Address Address { get; set; }
        public virtual Transaction Transaction { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }

    public class Transaction
    {
        [Key]
        public int transactionID { get; set; }
        public string employeeID { get; set; }
        [ForeignKey("Job")]
        public int jobID { get; set; }
        public float paymentToEmployee { get; set; }
        public float paymentToProvider { get; set; }
        public DateTime date { get; set; }
        public virtual Job Job { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class Application
    {
        [Key, Column(Order = 0)]
        public string ApplicantID { get; set; }
        [Key, Column(Order = 1)]
        public int JobID { get; set; }
        public string Comment { get; set; }

        // Navigation properties.
        // Parents.
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Job Job { get; set; }
    }

    public class Ratings
    {
        [Key, Column(Order = 0)]
        public string employeeID { get; set; }
        [Key, Column(Order = 1)]
        public int jobID { get; set; }
        public float score { get; set; }
        public string review { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
            .HasOne(a => a.Address) // Parent
            .WithMany(j => j.Jobs) // Child
            .HasForeignKey(fk => new { fk.addressID })
            .OnDelete(DeleteBehavior.Restrict);

            //---------------------------------------------------------------
            // Define composite primary keys.
            modelBuilder.Entity<Application>()
                .HasKey(app => new { app.ApplicantID, app.JobID });

            modelBuilder.Entity<Ratings>()
                .HasKey(app => new { app.employeeID, app.jobID });

            //---------------------------------------------------------------
            // Define foreign keys here. Do not use foreign key annotations.
            modelBuilder.Entity<Application>()
                .HasOne(app => app.ApplicationUser) // Parent
                .WithMany(u => u.Applications) // Child
                .HasForeignKey(fk => new { fk.ApplicantID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Application>()
                .HasOne(app => app.Job) // Parent
                .WithMany(j => j.Applications) // Child
                .HasForeignKey(fk => new { fk.JobID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Transaction>()
                .HasOne(au => au.ApplicationUser) // Parent
                .WithMany(t => t.Transactions) // Child
                .HasForeignKey(fk => new { fk.employeeID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Job>()
                .HasOne(au => au.ApplicationUser) // Parent
                .WithMany(j => j.Jobs) // Child
                .HasForeignKey(fk => new { fk.employeeID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Job>()
                .HasOne(au => au.ApplicationUser) // Parent
                .WithMany(j => j.Jobs) // Child
                .HasForeignKey(fk => new { fk.employerID })
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Ratings>()
               .HasOne(r => r.ApplicationUser) // Parent
               .WithMany(t => t.Ratings) // Child
               .HasForeignKey(fk => new { fk.employeeID })
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }

        public DbSet<WebSecurityAssignment.ViewModels.ApplicationVM> ApplicationVM { get; set; }
    }
}
