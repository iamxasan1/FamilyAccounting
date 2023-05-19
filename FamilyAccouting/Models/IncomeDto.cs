using FamilyAccounting.Data.Entities;

namespace FamilyAccouting.Models
{
    public class IncomeDto
    {
        public DateTime CreatedAt { get; set; }
        public string Incometype { get; set; }
        public int AmountIncome { get; set; }
        public string Description { get; set; }
    }
}
