using task18;

namespace task19;

public class Program
{
    public static void Main()
    {
        var server = new ServerThread();

        Enumerable.Range(0, 5)
            .ToList()
            .ForEach(id => server.AddCommand(new TestCommand(id, 3)));

        Thread.Sleep(100);
        server.AddCommand(new HardStop(server));
    }
}
