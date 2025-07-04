using System.Reflection;
using FileSystemCommands;
using CommandRunner;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "World");
        Directory.CreateDirectory(Path.Combine(testDir, "TestDir2"));
        File.WriteAllText(Path.Combine(testDir, "TestDir2", "file3.txt"), "Hello World");

        var command = new DirectorySizeCommand(testDir);
        command.Execute();
        Assert.Equal(21, command.Length);

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute();

        Assert.NotNull(command.Files);
        Assert.Single(command.Files.ToList());
        Assert.Equal("file1.txt", command.Files.First().Name);

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void CommeandRunnerCorrect()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "World");
        Directory.CreateDirectory(Path.Combine(testDir, "TestDir2"));
        File.WriteAllText(Path.Combine(testDir, "TestDir2", "file3.txt"), "Hello World");

        var output = new StringWriter();
        Console.SetOut(output);
        
        var expectedValue = "Найденные файлы:\nfile1.txt\r\nРазмер каталога: 21\r\n";

        Programm.Main();

        Assert.Equal(expectedValue, output.ToString());

        Directory.Delete(testDir, true);

    }
}
