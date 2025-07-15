namespace task17;

public class ExceptionHandler
{
    public static void ProccesEx(ICommand command, Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message} в команде {command.GetType().Name}");
    }
}
