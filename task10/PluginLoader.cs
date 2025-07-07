using System.Reflection;

namespace task10;

public class PluginLoader(string path)
{
    private string _path = path;

    private List<Type> _findedPlugins = [];

    public void Load()
    {
        if (!Directory.Exists(_path)) throw new Exception("Неверный путь");
        
        var loadedDlls = Directory.GetFiles(_path, "*.dll")
            .Select(dll => Assembly.LoadFrom(dll));

        _findedPlugins = loadedDlls
            .SelectMany(dll => dll.GetTypes()
            .Where(t => t.GetCustomAttribute<PluginLoad>() != null))
            .ToList();

        if (_findedPlugins.Count == 0) throw new Exception("Плагины не найдены");

        SortByDependences()
            .Select(pl => Activator.CreateInstance(pl) as ICommand)
            .ToList()
            .ForEach(pl => pl?.Execute());
    }

    private List<Type> SortByDependences(List<Type>? Output = null)
    {
        if (Output == null) Output = [];
        int CanExecuted = 0;
        
        _findedPlugins
            .Where(pl => !Output.Contains(pl))
            .Where(pl => (pl.GetCustomAttribute<PluginLoad>()?.Dependences ?? []).All(d => Output.Contains(d)))
            .ToList()
            .ForEach(pl => { Output.Add(pl); CanExecuted++; });

        if (Output.Count < _findedPlugins.Count)
        {
            if (CanExecuted == 0) throw new Exception("Нельзя загрузить некоторые плагины");
            return SortByDependences(Output);
        }
        return Output;
    }
}