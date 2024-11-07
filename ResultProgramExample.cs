// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");



static void Main()
{
    var calculator = new Calculator();

    // Case 1: Successful division
    var result1 = calculator.DivideNumbers(10, 2);
    if (result1.IsSuccess)
    {
        Console.WriteLine($"Success! The result is: {result1.Value}");
    }
    else
    {
        Console.WriteLine($"Error: {result1.ErrorMessage}");
        if (result1.Exception != null)
        {
            Console.WriteLine($"Exception Details: {result1.Exception}");
        }
    }

    // Case 2: Division by zero, with exception
    var result2 = calculator.DivideNumbers(10, 0);
    if (result2.IsSuccess)
    {
        Console.WriteLine($"Success! The result is: {result2.Value}");
    }
    else
    {
        Console.WriteLine($"Error: {result2.ErrorMessage}");
        if (result2.Exception != null)
        {
            Console.WriteLine($"Exception Details: {result2.Exception}");
        }
    }
}

public class Calculator
{
    public Result<double> DivideNumbers(double numerator, double denominator)
    {
        try
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException("Attempted to divide by zero.");
            }

            double result = numerator / denominator;
            return Result<double>.Success(result);
        }
        catch (Exception ex)
        {
            // Return the error result with exception
            return Result<double>.Error("An error occurred during division.", ex);
        }
    }
}