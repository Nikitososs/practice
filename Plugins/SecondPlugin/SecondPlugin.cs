using task10;

namespace SecondPlugin;

[PluginLoad([])]
public class Plugin2 : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Выполнен второй плагин");
    }
}
