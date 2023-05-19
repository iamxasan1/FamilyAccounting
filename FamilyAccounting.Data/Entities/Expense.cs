namespace FamilyAccounting.Data.Entities;

public class Expense
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public EExpensesType ExpenseType { get; set; }
    public int ExpenseAmount { get; set; }
    public string Description { get; set; }
}

public enum EExpensesType
{
    Food,
    Healthcare,
    Transport,
    Connection,
    Enjoy,
    Others
}

