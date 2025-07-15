using task17;

namespace task17tests;

public class ServerThreadTests
{
    [Fact]
    public void HardStop_Correct_Ex_not_from_server_thread()
    {
        var server = new ServerThread();
        var ex = Assert.Throws<InvalidOperationException>(() => server.HardStop());
        Assert.Equal("Команда HardStop успешно выполняюется только в потоке, который она должна остановить", ex.Message);
    }

    [Fact]
    public void SoftStop_Correct_Ex_not_from_server_thread()
    {
        var server = new ServerThread();
        var ex = Assert.Throws<InvalidOperationException>(() => server.SoftStop());
        Assert.Equal("Команда SoftStop успешно выполняюется только в потоке, который она должна остановить", ex.Message);
    }

    [Fact]
    public void HardStop_Correct_Stops_server()
    {
        var server = new ServerThread();
        var command = new TestCommand();

        server.AddCommand(new HardStop(server));
        server.AddCommand(command);

        Thread.Sleep(100);
        Assert.False(command.Executed);
    }

    [Fact]
    public void SoftStop_Correct_Stops_server_and_commands_executes()
    {
        var server = new ServerThread();
        var сommand1 = new TestCommand();
        var сommand2 = new TestCommand();

        server.AddCommand(сommand1);
        server.AddCommand(new SoftStop(server));
        server.AddCommand(сommand2);

        Thread.Sleep(100);
        Assert.True(сommand1.Executed);
        Assert.True(сommand2.Executed);
    }
    
    [Fact]
    public void ExceptionHandler_Test()
    {
        var server = new ServerThread();
        var badCommand = new BadCommand();

        var output = new StringWriter();
        Console.SetOut(output);

        server.AddCommand(badCommand);
        server.AddCommand(new SoftStop(server));

        Thread.Sleep(100);
        Assert.Equal("Ошибка: Сломанная команда выбросила ошибку в команде BadCommand\n", output.ToString());
    }
}
