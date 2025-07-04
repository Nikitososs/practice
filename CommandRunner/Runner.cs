using System.Reflection;
using CommandLib;

namespace CommandRunner;

public class Programm
{
    public static void Main()
    {
        Assembly FileSystemCommands = Assembly.LoadFrom("FileSystemCommands.dll")!;
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");

        Type FindFilesCommand = FileSystemCommands.GetType("FileSystemCommands.FindFilesCommand")!;
        Type DirectorySizeCommand = FileSystemCommands.GetType("FileSystemCommands.DirectorySizeCommand")!;

        var command = Activator.CreateInstance(FindFilesCommand, testDir, "*.txt") as ICommand;
        command?.Execute();

        var command2 = Activator.CreateInstance(DirectorySizeCommand, testDir) as ICommand;
        command2?.Execute();

        var files = FindFilesCommand.GetProperty("Files")!.GetValue(command) as FileInfo[];
        var length = DirectorySizeCommand.GetProperty("Length")!.GetValue(command2) as long?;

        if (files != null) Console.WriteLine($"Найденные файлы:\n{String.Join("\n", files.Select(f => f.Name))}");
        else Console.WriteLine("Указан неверный путь");

        if (length != null) Console.WriteLine($"Размер каталога: {length}");
        else Console.WriteLine("Указан несуществующий каталог");
    }
}
