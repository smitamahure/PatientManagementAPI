namespace PatientManagementAPI.DTO
{
    public class PatientResponseDto
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? BloodGroup { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
