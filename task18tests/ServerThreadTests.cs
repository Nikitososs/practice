using task18;

namespace task18tests;

public class ServerThreadTests
{
    [Fact]
    public void LongCommand_Correct_Executions_Count()
    {
        var server = new ServerThread();
        var longCommand = new TestLongCommand(3);
        server.AddCommand(longCommand);
        server.AddCommand(longCommand);

        Thread.Sleep(300);
        Assert.Equal(0, longCommand.ExecutionsToComplete);
    }

    [Fact]
    public void Correct_RobinRound_Scheduler_Test()
    {
        var scheduler = new Scheduler();

        var сommand1 = new TestCommand();
        var longCommand1 = new TestLongCommand(2);
        var сommand2 = new TestCommand();
        var longCommand2 = new TestLongCommand(2);

        scheduler.Add(сommand1);
        scheduler.Add(longCommand1);
        scheduler.Add(сommand2);
        scheduler.Add(longCommand2);

        var select1 = scheduler.Select();
        Assert.Equal(сommand1, select1);
        select1!.Execute();
        Thread.Sleep(100);

        var select2 = scheduler.Select();
        Assert.Equal(longCommand1, select2);
        select2!.Execute();
        Thread.Sleep(100);

        var select3 = scheduler.Select();
        Assert.Equal(сommand2, select3);
        select3!.Execute();
        Thread.Sleep(100);

        var select4 = scheduler.Select();
        Assert.Equal(longCommand2, select4);
        select4!.Execute();
        Thread.Sleep(100);

        var select5 = scheduler.Select();
        Assert.Equal(longCommand1, select5);
        select5!.Execute();
        Thread.Sleep(100);

        var select6 = scheduler.Select();
        Assert.Equal(longCommand2, select6);
        select6!.Execute();
        Thread.Sleep(100);

        Assert.Equal(0, longCommand1.ExecutionsToComplete);
        Assert.Equal(0, longCommand2.ExecutionsToComplete);

        Assert.False(scheduler.HasCommand());
    }

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
        Assert.Equal("Ошибка: Сломанная команда выбросила ошибку в команде BadCommand\r\n", output.ToString());
    }
}
