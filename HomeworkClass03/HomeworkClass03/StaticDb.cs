using HomeworkClass03.Models;

namespace HomeworkClass03
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book(){Title = "To Kill a Mockingbird", Author = "Harper Lee"},
            new Book(){Title = "Pride and Prejudice", Author = "Jane Austen"},
            new Book(){Title = "Jane Eyre", Author = "Charlotte Brontë"},
            new Book(){Title = "The Great Gatsby", Author = "F. Scott Fitzgerald"},

        };
    }
}
