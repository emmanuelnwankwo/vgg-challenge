using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Models.Entities;
using Action = WebApp.Data.Models.Entities.Action;

namespace WebApp.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasIndex(b => b.Name)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(b => b.Username)
                .IsUnique();
            modelBuilder.Entity<Action>()
            .HasOne(p => p.Project)
            .WithMany()
            .HasForeignKey(p => p.Project_Id);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Action> Actions { get; set; }
    }
}
