namespace DisCourseW.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorUserName { get; set; } // Hiển thị UserName thay vì chỉ có ID
        public int CourseId { get; set; }
    }

}
