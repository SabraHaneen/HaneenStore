namespace HaneenStore.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public BookModel BookModel { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
