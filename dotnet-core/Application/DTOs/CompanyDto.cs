namespace XMP.Application.DTOs
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Additional properties for DTO use, if needed
        // public string AdditionalProperty { get; set; }
    }
}
