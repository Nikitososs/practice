using task07;
using System.Reflection;
using System.Linq;

namespace task07;

public class ClassInfo
{
    public DisplayNameAttribute? NameAtt { get; }
    public VersionAttribute? VersionAtt { get; }
    public IEnumerable<MethodInfo> Methods { get; } = [];
    public IEnumerable<PropertyInfo> Properties { get; } = [];

    public ClassInfo(DisplayNameAttribute? name, VersionAttribute? version, IEnumerable<MethodInfo> methods, IEnumerable<PropertyInfo> properties)
    {
        NameAtt = name;
        VersionAtt = version;
        Methods = methods;
        Properties = properties;
    }
}

public class ReflectionHelper
{
    private Type _type;

    public ReflectionHelper(Type type)
    {
        _type = type;
    }

    public ClassInfo GetClassInfo()
    => new ClassInfo(
            _type.GetCustomAttribute<DisplayNameAttribute>(),
            _type.GetCustomAttribute<VersionAttribute>(),
            _type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName),
            _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    );

    public void PrintTypeInfo()
    {
        ClassInfo classInfo = GetClassInfo();
        var version = classInfo.VersionAtt;
        var dispVersion = $"{version?.Major}.{version?.Minor}" ?? "0.0";

        Console.WriteLine($"Отображаемое имя класса: {classInfo.NameAtt?.DisplayName ?? "Отображаемое имя класса отстуствует"}");

        Console.WriteLine($"Версия класса: {dispVersion}");

        Console.WriteLine($"\nСписок методов:\n{String.Join("\n",
            classInfo.Methods.Select(m => $"Название: {m.Name}\nОтображаемое имя: {m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "Отображаемое имя метода отстуствует"}\nПринимаемые параметры: {String.Join(", ", m.GetParameters().Select(p => p.Name ?? string.Empty))}\n"))}");

        Console.WriteLine($"\nСписок свойств:\n{String.Join("\n",
            classInfo.Properties.Select(m => $"Название: {m.Name}\nОтображаемое имя: {m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "Отображаемое имя свойства отстуствует"}\n"))}");
    }
}
