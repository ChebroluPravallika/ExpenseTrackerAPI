

using AutoMapper;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Models;
using ExpenseTracker.Infrastructure.Interfaces;
using ExpenseTracker.Infrastructure.Models;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Application.Services
{
    public class TransactionService : IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContext;

        public TransactionService(IExpenseRepository expenseRepository, IMapper mapper, IHttpContextAccessor httpContext)
        {
            this.expenseRepository = expenseRepository;
            this._mapper = mapper;
            this.httpContext = httpContext;
        }

        public Task AddTransaction(TransactionDTO transactionDTO)
        {
            int userId = Convert.ToInt32(this.httpContext.HttpContext.User.FindFirst("userId").Value);
            var transaction = _mapper.Map<Transaction>(transactionDTO);
            transaction.UserId = userId;
            return expenseRepository.AddTransaction(transaction);
        }

        public Task DeleteTransaction(string transaction_id)
        {
            return expenseRepository.DeleteTransaction(transaction_id);
        }

        public Task<List<Transaction>> GetAllTransactions()
        {
            return expenseRepository.GetAllTransactions();
        }
    }
}
