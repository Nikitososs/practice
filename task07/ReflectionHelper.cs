using task07;
using System.Reflection;
using System.Linq;

namespace task07;

public class ReflectionHelper
{
    private Type _type;

    public ReflectionHelper(Type type)
    {
        _type = type;
    }

    public void PrintTypeInfo()
    {
        var version = _type.GetCustomAttribute<VersionAttribute>();
        var dispVersion = $"{version?.Major}.{version?.Minor}" ?? "0.0";

        Console.WriteLine($"Отображаемое имя класса: {_type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "Отображаемое имя класса отстуствует"}");

        Console.WriteLine($"Версия класса: {dispVersion}");

        Console.WriteLine($"\nСписок методов:\n{String.Join("\n",
            _type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName)
            .Select(m => $"Название: {m.Name}\nОтображаемое имя: {m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "Отображаемое имя метода отстуствует"}\nПринимаемые параметры: {String.Join(", ", m.GetParameters().Select(p => p.Name ?? string.Empty))}\n"))}");

        Console.WriteLine($"\nСписок свойств:\n{String.Join("\n",
            _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Select(m => $"Название: {m.Name}\nОтображаемое имя: {m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "Отображаемое имя свойства отстуствует"}\n"))}");
    }
}
