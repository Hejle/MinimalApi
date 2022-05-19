using System.Runtime.Serialization;

namespace MinimalApi.Common.Models;

[Serializable]
public class BookException : Exception
{
    public BookException()
    {
    }

    public BookException(string message)
        : base(message)
    {
    }

    public BookException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected BookException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}