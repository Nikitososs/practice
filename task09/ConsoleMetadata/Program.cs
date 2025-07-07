using System.Reflection;
using LibInfoNs;

namespace ConsoleMetadata;
public class Program
{
    public static void Main(string[] args)
    {
        string? path;
        if (args.Length == 0)
        {
            Console.WriteLine("Путь не был указан, введите путь:");
            path = Console.ReadLine();
        }
        else path = args[0];
        if (path != null)
        {
            Assembly lib = Assembly.LoadFrom(path);
            LibInfo libInfo = new LibInfo(lib);
            libInfo.Print();
        }
    }
}
