namespace FirstWeb_API.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Details { get; set; }
        public double Rate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
