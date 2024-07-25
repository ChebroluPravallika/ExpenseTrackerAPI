using ExpenseTracker.Application.Models;
using ExpenseTracker.Infrastructure.Models;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IExpenseService
    {
        Task AddTransaction(TransactionDTO transaction);

        Task DeleteTransaction(string transaction_id);

        Task<List<Transaction>> GetAllTransactions();
    }
}