using task10;
using SecondPlugin;

namespace FourthPlugin;


[PluginLoad([typeof(Plugin2)])]
public class Plugin4 : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Выполнен четвёртый плагин");
    }
}
