// See https://aka.ms/new-console-template for more information
public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
    public T Value { get; }
    public string ErrorMessage { get; }
    public Exception Exception { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        ErrorMessage = null;
        Exception = null;
    }

    private Result(string errorMessage, Exception exception = null)
    {
        IsSuccess = false;
        Value = default;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    public static Result<T> Success(T value) => new Result<T>(value);

    // Error method with a custom error message and optional exception
    public static Result<T> Error(string errorMessage, Exception exception = null)
        => new Result<T>(errorMessage, exception);

    // Error method with no message or exception
    public static Result<T> Error()
        => new Result<T>("An unspecified error occurred.");

    // Error method with only an exception
    public static Result<T> Error(Exception ex)
        => new Result<T>(ex.Message, ex);

    public override string ToString()
    {
        if (IsSuccess) return $"Success: {Value}";

        return Exception != null
            ? $"Error: {ErrorMessage} | Exception: {Exception.Message}"
            : $"Error: {ErrorMessage}";
    }
}