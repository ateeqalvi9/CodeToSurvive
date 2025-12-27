using CodeToSurvive.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeToSurvive.DLL;

namespace CodeToSurvive.DLL
{
    class GameDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<OwnedOutfit> OwnedOutfits { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<CourseProgress> CourseProgress { get; set; }
        public DbSet<Meal> Meals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.Id);

            modelBuilder.Entity<OwnedOutfit>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OwnedOutfit>()
                .HasOne(o => o.Player)
                .WithMany(p => p.OwnedOutfit)
                .HasForeignKey(o => o.PlayerId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseProgress>()
                .HasKey(cp => new { cp.PlayerId, cp.CourseId });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Meal>()
                .HasKey(mp => mp.MealId);

            modelBuilder.Entity<Meal>()
                        .HasOne(static mp => mp.Player)
                        .WithMany()
                        .HasForeignKey(mp => mp.PlayerId);
        }
    }
}
