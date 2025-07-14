namespace BibliotecaBLL.DTOs
{
    public record BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Summary { get; set; }
        public int PublisherId { get; set; }
    }
}
