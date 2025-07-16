namespace WebHarcamaTakip.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }  // <-- Bu satır olmalı!
        public string ExpenseType { get; set; }   // "Gelir" veya "Gider"

    }
}