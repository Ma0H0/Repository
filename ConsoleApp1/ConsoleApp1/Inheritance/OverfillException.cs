namespace ConsoleApp1.Inheritance;

public class OverfillException:Exception
{
    public OverfillException(string message) : base(message)
    {
        Console.WriteLine("Error: " + message);
    }    
}