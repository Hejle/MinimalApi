using MinimalApi.Common.Models;
using MinimalApi.Database.Context;

namespace MinimalApi.Database.Services
{
    public interface IBookDataAccess
    {
        Book? GetBook(string isbn);
        void CreateBook(Book book);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(string searchTerm);
        bool DeleteBook(string isbn);
        bool UpdateBook(string isbn, Book book);
    }

    public class BookDataAccess : IBookDataAccess
    {
        private readonly BookContext _context;

        public BookDataAccess(BookContext bookContext)
        {
            _context = bookContext;
        }

        public void CreateBook(Book book)
        {      
            _context.Add(book);
            _context.SaveChanges();
        }

        public bool DeleteBook(string isbn)
        {
            var book = _context.Books.FirstOrDefault(book => book.Isbn.Equals(isbn));
            if (book == null)
                return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book? GetBook(string isbn)
        {
            var book = _context.Books.FirstOrDefault(book => book.Isbn.Equals(isbn));
            return book;
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBook(string isbn, Book book)
        {
            var bookToBeUpdated = _context.Books.SingleOrDefault(book => book.Isbn.Equals(isbn));
            if(bookToBeUpdated == null)
            {
                return false;
            }
            bookToBeUpdated = book;
            _context.SaveChanges();
            return true;
        }
    }
}
