using task17;

namespace task17tests;

public class TestCommand : ICommand
{
    public bool Executed { get; private set; }
    public void Execute()
    {
        Executed = true;
    }
}

public class BadCommand : ICommand
{
    public void Execute()
    {
        throw new Exception("Сломанная команда выбросила ошибку");
    }
}
