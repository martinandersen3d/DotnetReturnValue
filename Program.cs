using ReturnUtils;

class Program
{
    public static void Main(string[] args)
    {
        var sum = Addition(3, 5);

        Console.WriteLine($"Value: {sum.Value}");
        if (sum.Success) Console.WriteLine("Good Job");
        if (sum.Failed) Console.WriteLine("Ohh Nooo");

        var divide = Divide(5, 0);

        Console.WriteLine($"Value: {divide.Value}");
        if (divide.Success) Console.WriteLine("Good Job");
        if (divide.Failed) Console.WriteLine("Ohh Nooo - You devided with 0");

        var random = Random();
    }

    // Define return type to INT
    public static ReturnValue<int> Addition(int x, int y)
    {
        var _return = new ReturnValue<int>();
        _return.Value = x + y;
        return _return;
    }

    // Define return type to DOUBLE
    public static ReturnValue<double> Divide(int x, int y)
    {
        var _return = new ReturnValue<double>();
        try
        {
            _return.Value = x / y;
        }
        catch (DivideByZeroException e)
        {
            _return.Error = e;
        }
        return _return;
    }

    // Define return type to INT or NULL
    public static ReturnValue<int?> Random()
    {
        var _return = new ReturnValue<int?>();

        Random rnd = new Random();
        int number = rnd.Next(1, 10);
        if (number < 5)
        {
            _return.Value = 1;
        }
        else { _return.Value = null; }

        return _return;
    }
}