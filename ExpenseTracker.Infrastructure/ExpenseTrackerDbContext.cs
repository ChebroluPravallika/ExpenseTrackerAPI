using ExpenseTracker.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure
{
    public class ExpenseTrackerDbContext : IdentityDbContext<IdentityUser>
    {

        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        //public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.OnTransactionsCreating();
            modelBuilder.OnUsersCreating();
        }
    }
}
