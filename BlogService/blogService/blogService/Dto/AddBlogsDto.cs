namespace blogService.Dto
{
    public class AddBlogsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
