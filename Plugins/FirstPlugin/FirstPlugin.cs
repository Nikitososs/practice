using task10;

namespace FirstPlugin;

[PluginLoad([])]
public class Plugin1 : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Выполнен первый плагин");
    }
}
