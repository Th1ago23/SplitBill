using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Database
{
    public class DbConfig:DbContext
    {
        public DbConfig(DbContextOptions<DbConfig> options) : base(options) { }

        DbSet<User> Users { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(i => i.Groups)
                .WithMany(g => g.Users);

            modelBuilder.Entity<Expense>()
                .HasMany(i => i.Participants)
                .WithMany(f => f.ParticipatedInExpenses);

            modelBuilder.Entity<Expense>()
                .HasOne(i => i.Payer)
                .WithMany()
                .HasForeignKey(e => e.PaidByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
