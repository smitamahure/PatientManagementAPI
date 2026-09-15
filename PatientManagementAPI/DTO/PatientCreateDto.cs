using System.ComponentModel.DataAnnotations;

namespace PatientManagementAPI.DTO
{
    public class PatientCreateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? BloodGroup { get; set; }
    }
}
