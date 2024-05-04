namespace SimpleInventoryManagementSystem;

public class ConsoleDisplay
{
    public static void MessageDisplay(string message, bool newLine = true)
    {
        if (newLine)
        {
            Console.WriteLine(message);
        }
        else
        {
            Console.Write(message);
        }
    }

    public static void ClearScreen()
    {
        Console.Clear();
    }

    public static void SetForegroundColor(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }

    public static void ResetColor()
    {
        Console.ResetColor();
    }
}