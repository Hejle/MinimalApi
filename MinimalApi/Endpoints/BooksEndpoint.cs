using FluentValidation;
using MinimalApi.Common.Models;
using MinimalApi.Logic.Services;

namespace MinimalApi.Endpoints;

public static class BooksEndpoint
{
    public static void UseBookEndpoint(this IEndpointRouteBuilder app, IConfiguration configuration)
    {
        //var scopeRequiredByApi = configuration["AzureAd:Scopes"];

        app.MapGet("books/{isbn}", GetBook)
            .Produces<Book>(200).Produces(404)
            .WithName("GetBook")
            .AllowAnonymous();

        app.MapGet("books", GetBooks)
            .Produces<IEnumerable<Book>>(200)
            .WithName("GetAllBooks")
            .AllowAnonymous();

        app.MapPut("books/{isbn}", UpdateBook)
            .Accepts<Book>("application/json")
            .Produces<Book>(200).Produces(404)
            .WithName("UpdateBook")
            .AllowAnonymous();

        app.MapDelete("books/{isbn}", DeleteBook)
            .Produces(200).Produces(404)
            .WithName("DeleteBook")
            .AllowAnonymous();

        app.MapPost("books", CreateBook)
            .Accepts<Book>("application/json")
            .Produces<Book>(201)
            .Produces<HttpValidationProblemDetails>(400)
            .Produces<ApiException>(400)
            .WithName("CreateBook")
            .AllowAnonymous();
        //.RequireAuthorization();
    }

    private static IResult CreateBook(Book book, IBookService bookService, ILogger<Program> logger)
    {
        try
        {
            bookService.CreateBook(book);
            return Results.Created($"/books/{book.Isbn}", book);
        }
        catch (ValidationException validationException)
        {
            var problems = validationException.Errors.ToDictionary(error => error.PropertyName, error => new string[] { error.ErrorMessage });
            return Results.ValidationProblem(problems);
        }
        catch (BookException bookException)
        {
            return Results.BadRequest(new ApiException(bookException));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error when creating book");
            return Results.BadRequest(new ApiException(exception));
        }
    }

    private static IResult GetBook(string isbn, IBookService bookService)
    {
        var book = bookService.GetBook(isbn);
        if (book == null)
            return Results.NotFound();
        return Results.Ok(book);
    }

    private static IResult GetBooks(IBookService bookService)
    {
        var books = bookService.GetAllBooks();
        return Results.Ok(books);
    }

    private static IResult DeleteBook(string isbn, IBookService bookService)
    {
        var deleted = bookService.DeleteBook(isbn);
        return deleted ? Results.Ok() : Results.NotFound();
    }

    private static IResult UpdateBook(string isbn, Book book, IBookService bookService)
    {
        var updatedBook = bookService.UpdateBook(isbn, book);
        return updatedBook ? Results.Ok(book) : Results.NotFound();
    }
}