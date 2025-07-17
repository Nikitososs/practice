namespace task18;

public class HardStop(ServerThread server) : ICommand
{
    private readonly ServerThread _server = server;

    public void Execute()
    {
        _server.HardStop();
    }
}

public class SoftStop(ServerThread server) : ICommand
{
    private readonly ServerThread _server = server;

    public void Execute()
    {
        _server.SoftStop();
    }
}
