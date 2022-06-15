using FluentValidation;
using MinimalApi.Common.Models;
using MinimalApi.Database.Services;

namespace MinimalApi.Logic.Services;

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
    private readonly IValidator<Book> _bookValidator;

    public BookService(IBookDataAccess bookDataAccess,
        IValidator<Book> bookValidator)
    {
        _bookDataAccess = bookDataAccess;
        _bookValidator = bookValidator;
    }

    public void CreateBook(Book book)
    {
        var validationResult = _bookValidator.Validate(book);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (GetBook(book.Isbn) != null)
        {
            throw new MinimalApiException("A book with the following ISBN already exists");
        }

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