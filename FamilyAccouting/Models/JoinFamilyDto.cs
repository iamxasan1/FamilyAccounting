using FamilyAccounting.Data.Entities;

namespace FamilyAccouting.Models
{
    public class JoinFamilyDto
    {
        public Guid FamilyId { get; set; }
        public EUserType UserType { get; set; }
    }
}
