using task10;
using FirstPlugin;
using SecondPlugin;

namespace ThirdPlugin;

[PluginLoad([typeof(Plugin1), typeof(Plugin2)])]
public class Plugin3 : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Выполнен третий плагин");
    }
}