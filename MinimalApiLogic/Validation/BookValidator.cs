using FluentValidation;
using MinimalApi.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Logic.Validation;
internal class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(book => book.PageCount).GreaterThan(0);
        RuleFor(book => book.Title).NotEmpty();
        RuleFor(book => book.ShortDescription).NotEmpty();
        RuleFor(book => book.Author).NotEmpty();
        RuleFor(book => book.Isbn).SetValidator(new IsbnValidator());
    }
}
