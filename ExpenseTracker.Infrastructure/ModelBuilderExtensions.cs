using ExpenseTracker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder OnTransactionsCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).IsRequired();
            modelBuilder.Entity<Transaction>().Property(t => t.Status).IsRequired();
            modelBuilder.Entity<Transaction>().Property(t => t.PaymentMethod).IsRequired();
            modelBuilder.Entity<Transaction>().Property(t => t.Date).IsRequired();
            modelBuilder.Entity<Transaction>().Property(t => t.TransactionId).IsRequired();
            modelBuilder.Entity<Transaction>().HasOne(t => t.User).WithMany(u => u.Transactions).HasForeignKey(t=>t.UserId).IsRequired();
            modelBuilder.Entity<Transaction>().Property(t => t.UserId).IsRequired();
            return modelBuilder;
        }

        public static ModelBuilder OnUsersCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().HasMany(u => u.Transactions).WithOne(t => t.User);
            return modelBuilder;
        }
    }
}