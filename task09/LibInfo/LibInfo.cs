using System.Reflection;

using task07;
namespace LibInfoNs;

public class Parameter(string name, Type type)
{
    public string Name { get; } = name;
    public Type ParameterType { get; } = type;
    public void Print()
    {
        if (ParameterType.IsGenericType)
        {
            Console.WriteLine($"Тип: {ParameterType.Name.Remove(ParameterType.Name.Length - 2)}<{string.Join(", ", ParameterType.GetGenericArguments().Select(t => t.Name))}>, Имя: {Name}");
        }
        else Console.WriteLine($"Тип: {ParameterType.Name}, Имя: {Name}");
    }
}

public class LibConstructor(string name, IEnumerable<Parameter> parameters)
{
    public string Name { get; } = name;
    public IEnumerable<Parameter> Parameters { get; } = parameters;
    public void Print()
    {
        Console.WriteLine($"Имя: {Name}\nПараметры:");
        Parameters.ToList().ForEach(p => p.Print());
        Console.WriteLine();
    }
}

public class LibMethod(string name, string returntype, IEnumerable<Parameter> parameters)
{
    public string Name { get; } = name;
    public string ReturnType { get; } = returntype;
    public IEnumerable<Parameter> Parameters { get; } = parameters;
    public void Print()
    {
        Console.WriteLine($"Имя: {Name}\nПараметры: ");
        Parameters.ToList().ForEach(p => p.Print());
        Console.WriteLine($"\nВозвращаемый тип: {ReturnType}\n");
    }
}

public class LibType(string name, IEnumerable<string> attributes, IEnumerable<LibMethod> methods, IEnumerable<LibConstructor> constructors)
{
    public string Name { get; } = name;
    public IEnumerable<string> Attributes { get; } = attributes;
    public IEnumerable<LibMethod> Methods { get; } = methods;
    public IEnumerable<LibConstructor> Constructors { get; } = constructors;
    public void Print()
    {
        Console.WriteLine($"Класс: {Name}");
        Console.WriteLine($"Аттрибуты:\n{String.Join("\n", Attributes.Select(a => a))}");
        Console.WriteLine();
        Console.WriteLine("Конструкторы:");
        Constructors.ToList().ForEach(p => p.Print());
        Console.WriteLine();
        Console.WriteLine("Методы:");
        Methods.ToList().ForEach(p => p.Print());
        Console.WriteLine("\n=====\n");
    }
}

[DisplayNameAttribute("Класс с метаданными передаваемой библиотеки")]
[VersionAttribute(1, 0)]
public class LibInfo
{
    private Assembly _lib;
    public IEnumerable<LibType> FindedTypes { get; }

    public LibInfo(Assembly lib)
    {
        _lib = lib;
        FindedTypes = lib.GetTypes()
            .Where(t => t.IsPublic)
            .Select(t => new LibType(
                t.Name,
                t.GetCustomAttributes(true)
                    .Where(a => a.GetType().Name != "NullableContextAttribute" && a.GetType().Name != "NullableAttribute")
                    .Select(a => a.GetType().Name),
                t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .Select(m => new LibMethod(m.Name,
                        m.ReturnType.Name,
                        m.GetParameters()
                            .Select(p => new Parameter(p.Name ?? "Нет имени", p.ParameterType)))),
                t.GetConstructors().Select(c => new LibConstructor(
                    t.Name,
                    c.GetParameters()
                        .Select(p => new Parameter(p.Name ?? "Нет имени", p.ParameterType))))));
    }

    public void Print()
    {
        Console.WriteLine($"Библиотека: {_lib.FullName}\n");
        Console.WriteLine($"Список классов:\n");
        Console.WriteLine("=====\n");
        FindedTypes.ToList().ForEach(t => t.Print());
    }
}
