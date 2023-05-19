namespace FamilyAccounting.Data.Entities;

public class Income
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public EIncomeType IncomeType { get; set; }
    public int AmountIncome { get; set; }
    public string? Description { get; set; }
}

public enum EIncomeType
{
    Salary,
    Ranting,
    Others
}