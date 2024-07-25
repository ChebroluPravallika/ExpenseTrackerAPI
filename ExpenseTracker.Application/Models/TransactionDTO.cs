namespace ExpenseTracker.Application.Models
{
    public class TransactionDTO
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod  { get; set; }
    }
}
