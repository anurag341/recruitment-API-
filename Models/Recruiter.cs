using System.ComponentModel.DataAnnotations;

namespace RecruiterApi.Models
{
    public class Recruiter
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string ContactNumber { get; set; } = string.Empty;

        [Range(0, 180)]
        public int NoticePeriodDays { get; set; }

        [Range(0, 200)]
        public decimal ExpectedCtcLpa { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}