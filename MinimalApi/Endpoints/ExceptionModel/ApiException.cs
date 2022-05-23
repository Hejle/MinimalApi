namespace MinimalApi.Endpoints.ExceptionModel;

public class ApiException
{
    public string ExceptionType { get; set; }
    public string ErrorMessage { get; set; }

    public ApiException(Exception exception)
    {
        ExceptionType = exception.GetType().Name;
        ErrorMessage = exception.Message;
    }
}