using ExpenseTracker.Infrastructure.Models;

namespace ExpenseTracker.Infrastructure.Interfaces
{
    public interface IExpenseRepository
    {
        Task AddTransaction(Transaction transaction);

        Task DeleteTransaction(string transaction_id);

        Task<List<Transaction>> GetAllTransactions();

        //Task GetTransactionById(int transaction)
    }
}
