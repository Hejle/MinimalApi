using MinimalApi.Common.Models;
using MinimalApi.Database.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Logic.Services
{
    public interface IBookService
    {
        Book? GetBook(string isbn);
        void CreateBook(Book book);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(string searchTerm);
        bool DeleteBook(string isbn);
        bool UpdateBook(string isbn, Book book);
    }

    internal class BookService : IBookService
    {
        private readonly IBookDataAccess _bookDataAccess;

        public BookService(IBookDataAccess bookDataAccess)
        {
            _bookDataAccess = bookDataAccess;
        }

        public void CreateBook(Book book)
        {
            if (book == null)
                throw new ValidationException("Book was null");
            if(book.PageCount <= 0)
                throw new ValidationException("Pagecount must be more than zero");
            _bookDataAccess.CreateBook(book);
        }

        public bool DeleteBook(string isbn)
        {
            return _bookDataAccess.DeleteBook(isbn);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookDataAccess.GetAllBooks();
        }

        public Book? GetBook(string isbn)
        {
            return _bookDataAccess.GetBook(isbn);
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBook(string isbn, Book book)
        {
            return _bookDataAccess.UpdateBook(isbn, book);
        }
    }
}
