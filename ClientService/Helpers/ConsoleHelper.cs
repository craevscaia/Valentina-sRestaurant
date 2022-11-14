namespace Client.Helpers;

public static class ConsoleHelper
{
    public static Task Print (string message)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
    public static Task Print (string message, ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}