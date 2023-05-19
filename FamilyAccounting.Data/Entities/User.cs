using Microsoft.AspNetCore.Identity;

namespace FamilyAccounting.Data.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public EUserType UserType { get; set; }
    public string? PhotoUrl { get; set; }
    public List<Income>? Incomes { get; set; }
    public List<Expense>? Expenses { get; set; }
    public Guid? FamilyId { get; set; }
    public Family? Family { get; set; }
}

public enum EUserType
{
    Father,
    Mother,
    Son,
    Doughter
}