using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MinimalApi.Database.Converters;

internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() :
        base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
    {
    }
}