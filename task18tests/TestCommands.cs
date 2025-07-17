using task18;

namespace task18tests;

public class TestCommand : ICommand
{
    public bool Executed { get; private set; }
    public void Execute()
    {
        Executed = true;
    }
}

public class TestLongCommand(int executionsToComplete) : ICommandLong
{
    public int ExecutionsToComplete { get; private set; } = executionsToComplete;
    public void Execute() { Thread.Sleep(50); ExecutionsToComplete--; }
}

public class BadCommand : ICommand
{
    public void Execute()
    {
        throw new Exception("Сломанная команда выбросила ошибку");
    }
}
