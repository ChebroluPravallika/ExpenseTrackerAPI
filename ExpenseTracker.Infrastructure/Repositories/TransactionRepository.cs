using ExpenseTracker.Infrastructure.Interfaces;
using ExpenseTracker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class TransactionRepository : IExpenseRepository
    {

        public readonly ExpenseTrackerDbContext dbContext;

        public TransactionRepository(ExpenseTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await dbContext.Transactions.AddAsync(transaction);
        }

        public async Task DeleteTransaction(string transaction_id)
        {
            var transaction = await dbContext.Transactions.FindAsync(transaction_id);
            dbContext.Transactions.Remove(transaction);
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            var transactions = dbContext.Transactions;
            return await transactions.ToListAsync();
        }
    }
}
