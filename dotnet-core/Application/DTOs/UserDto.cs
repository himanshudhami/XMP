namespace XMP.Application.DTOs
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordSentAt { get; set; }
        public DateTime? RememberCreatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Avatar { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
