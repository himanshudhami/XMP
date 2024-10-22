namespace XMP.Application.DTOs
{
    public class StatusDto
    {
        public long Id { get; set; }
        public string? StatusValue { get; set; } // Renamed to avoid conflict with the class name
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
