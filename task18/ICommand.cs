namespace task18;

public interface ICommand
{
    void Execute();
}

public interface ICommandLong : ICommand
{
    int ExecutionsToComplete { get; }
}
