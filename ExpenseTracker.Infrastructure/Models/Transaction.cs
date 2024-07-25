namespace ExpenseTracker.Infrastructure.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public required string TransactionId { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
