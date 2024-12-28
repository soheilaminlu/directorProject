namespace blogService.Dto
{
    public class UpdateBlogs
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
