using FamilyAccounting.Data.Entities;

namespace FamilyAccouting.Models
{
    public class ExpenseDto
    {
        public DateTime CreatedAt { get; set; }
        public string Expensetype { get; set; }
        public int AmountExpense { get; set; }
        public string Description { get; set; }
    }
}
