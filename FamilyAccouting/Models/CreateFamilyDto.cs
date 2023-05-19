using FamilyAccounting.Data.Entities;

namespace FamilyAccouting.Models;
public class CreateFamilyDto
{
    public EUserType CreatorType { get; set; }
    public string FamilyName { get; set; }
    public string Password { get; set;}
}
