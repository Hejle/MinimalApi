using System.Runtime.Serialization;

namespace MinimalApi.Common.Models;

[Serializable]
public class MinimalApiException : Exception
{
    public MinimalApiException()
    {
    }

    public MinimalApiException(string message)
        : base(message)
    {
    }

    public MinimalApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected MinimalApiException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}