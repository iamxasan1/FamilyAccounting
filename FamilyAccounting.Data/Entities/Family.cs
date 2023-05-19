namespace FamilyAccounting.Data.Entities;

public class Family
{
    public Guid Id { get; set; }
    public string FamilyName { get; set; }
    public string Password { get; set; }
    public List<User> Users { get; set; }
   
}
