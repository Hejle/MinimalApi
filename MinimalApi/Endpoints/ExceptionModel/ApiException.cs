namespace MinimalApi.Endpoints.ExceptionModel;

public class ApiExceptionModel
{
    public string ExceptionType { get; set; }
    public string ErrorMessage { get; set; }

    public ApiExceptionModel(Exception exception)
    {
        ExceptionType = exception.GetType().Name;
        ErrorMessage = exception.Message;
    }
}