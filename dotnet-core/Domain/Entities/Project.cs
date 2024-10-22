namespace XMP.Domain.Entities
{
    public class Project
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? KeyName { get; set; }
    }
}
