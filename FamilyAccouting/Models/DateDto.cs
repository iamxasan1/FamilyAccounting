namespace FamilyAccouting.Models
{
    public class DateDto
    {
        public DateTime? From { get; set; } = DateTime.MinValue;
        public DateTime? To { get; set; } = DateTime.MaxValue;
    }
}
