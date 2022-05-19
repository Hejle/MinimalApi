using FluentValidation;

namespace MinimalApi.Logic.Validation;
internal class IsbnValidator : AbstractValidator<string>
{
    public IsbnValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(isbnString => isbnString).Cascade(CascadeMode.Stop)
            .Must(ValidateIsbnFormat)
                .WithMessage("The ISBN must only contain integers and dashes.")
            .Must(ValidateIsbnLength)
                .WithMessage("The ISBN must contain 10 or 13 integers.")
            .Must(ValidateIsbnCheckSum)
                .WithMessage("The ISBN checksum does not not match expected value.");
    }

    private static int[]? GetIsbnIntArray(string isbn)
    {
        try
        {
            return isbn.Replace("-", "").Select(x => int.Parse(x.ToString())).ToArray();
        }
        catch (FormatException)
        {
            return default;
        }
    }

    private static bool ValidateIsbnFormat(string isbn)
    {
        var isbnArray = GetIsbnIntArray(isbn);

        return isbnArray is not null;
    }

    private static bool ValidateIsbnLength(string isbn)
    {
        var isbnArray = GetIsbnIntArray(isbn);

        if (isbnArray?.Length == 10 || isbnArray?.Length == 13)
        {
            return true;
        }
        return false;
    }

    private static bool ValidateIsbnCheckSum(string isbn)
    {
        var isbnArray = GetIsbnIntArray(isbn);

        if (isbnArray?.Count() == 10)
        {
            return CheckIsbn10(isbnArray);
        }

        if (isbnArray?.Count() == 13)
        {
            return CheckIsbn13(isbnArray);
        }

        return false;
    }

    private static bool CheckIsbn10(int[] isbn)
    {
        int sum = 0;
        var checkDigit = isbn[9];

        for (int i = 0; i < isbn.Length-1; i++)
        {
            sum += isbn[i] * (10-i);
        }

        return (11 - (sum % 11)) == checkDigit;
    }

    private static bool CheckIsbn13(int[] isbn)
    {
        var sum = 0;
        var checkDigit = isbn[12];

        for (int i = 0; i < isbn.Length-1; i++)
        {
            if(i%2 == 0)
            {
                sum += (isbn[i] * 1);
                continue;
            }
            sum += (isbn[i] * 3);
        }

        return 10 - (sum % 10) == checkDigit;
    }
}
