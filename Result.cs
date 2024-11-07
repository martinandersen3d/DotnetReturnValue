// See https://aka.ms/new-console-template for more information
using System.Text.Json.Serialization;

namespace ResultValue;

public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
    public T Value { get; }
    public string Message { get; }
    public Exception? Exception { get; }

    /// <summary>
    /// Succes Constructor
    /// </summary>
    private Result(T value, string message = "")
    {
        IsSuccess = true;
        Value = value;
        Message = message;
        Exception = null;
    }
    
    /// <summary>
    /// Error Constructor
    /// </summary>
    private Result(string message = "", Exception? exception = null)
    {
        IsSuccess = false;
        Value = default!;
        Message = message;
        Exception = exception;
    }

    public static Result<T> Success(T value) => new Result<T>(value);

    // Error method with a custom error message and optional exception
    public static Result<T> Error(string errorMessage, Exception? exception = null)
        => new Result<T>(errorMessage, exception);

    // Error method with no message or exception
    public static Result<T> Error()
        => new Result<T>("An unspecified error occurred.");

    // Error method with only an exception
    public static Result<T> Error(Exception ex)
        => new Result<T>(ex.Message, ex);

    /// <summary>
    /// On Succes: Return the Value
    /// On Error: Returns the defaultValue from the method parameter
    /// </summary>
    public T GetValueOrDefault(T defaultValue)
    {
        return IsSuccess ? Value : defaultValue;
    }

    public override string ToString()
    {
        if (IsSuccess)
            return $"Success: {Value} - {Message}";

        if (Exception != null)
            return $"Error: {Message} | Exception: {Exception.Message}";

        return $"Error: {Message}";
    }

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> into a successful <see cref="Result{T}"/>.
    /// This allows directly assigning or returning a value where <see cref="Result{T}"/> is expected.
    /// <code>
    /// public Result<int> ProcessNumber(int number)
    /// {
    ///     if (number < 0)
    ///         return new ArgumentException("Invalid number: must be positive"); // Implicitly converts to Result<int> error
    ///
    ///     return number * 2; // Implicitly converts to Result<int> success
    /// }
    ///
    /// Console.WriteLine(ProcessNumber(5));  // Output: Success: 10
    /// Console.WriteLine(ProcessNumber(-1)); // Output: Error: Invalid number: must be positive
    /// </code>
    /// </summary>
    /// <param name="value">The value to convert into a successful <see cref="Result{T}"/>.</param>
    public static implicit operator Result<T>(T value) => Success(value);

    /// <summary>
    /// Implicitly converts an <see cref="Exception"/> into an error <see cref="Result{T}"/>.
    /// This allows directly assigning or returning an exception where <see cref="Result{T}"/> is expected.
    /// <code>
    /// public Result<int> ProcessNumber(int number)
    /// {
    ///     if (number < 0)
    ///         return new ArgumentException("Invalid number: must be positive"); // Implicitly converts to Result<int> error
    ///
    ///     return number * 2; // Implicitly converts to Result<int> success
    /// }
    ///
    /// Console.WriteLine(ProcessNumber(5));  // Output: Success: 10
    /// Console.WriteLine(ProcessNumber(-1)); // Output: Error: Invalid number: must be positive
    /// </code>
    /// </summary>
    /// <param name="ex">The exception to convert into an error <see cref="Result{T}"/>.</param>
    public static implicit operator Result<T>(Exception ex) => Error(ex);
    
}

/// <summary>
/// Converts any value of type <typeparamref name="T"/> into a <see cref="Result{T}"/> instance,
/// wrapping the value as a successful result. Works for ValueTypes and ReferenceTypes.
/// </summary>
/// <code>
/// int number = 42;
/// Result<int> result = number.ToResult();
///
/// string text = "Hello";
/// Result<string> resultText = text.ToResult();
///
/// string nullText = null;
/// Result<string> resultNull = nullText.ToResult();
/// Console.WriteLine(resultNull); // Output: Success:
/// </code>
public static class ResultExtensions
{
public static Result<T> ToResult<T>(this T value) => Result<T>.Success(value);
}
