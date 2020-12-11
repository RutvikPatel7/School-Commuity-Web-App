using Assignment1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Data
{
    public class SchoolCommunityContext : DbContext
    {
        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Community> Communities { get; set; }

        public DbSet<CommunityMembership> CommunityMemberships { get; set; }
       // public object CommunityMemberships { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Community>().ToTable("Community");
            modelBuilder.Entity<CommunityMembership>().ToTable("CommunityMembership");

            modelBuilder.Entity<CommunityMembership>()
                .HasKey(c => new { c.StudentID, c.CommunityID });

        }
    }
}
