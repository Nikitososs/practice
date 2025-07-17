using task18;

namespace task19;

public class TestCommand(int id, int executionsToComplete) : ICommandLong
{
    int counter = 0;

    public int ExecutionsToComplete { get; private set; } = executionsToComplete;

    public void Execute()
    {
        Console.WriteLine($"Поток {id} вызов {++counter}");
        ExecutionsToComplete--;
    }
}
