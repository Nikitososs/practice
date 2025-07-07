using task10;

namespace task10tests;

public class PluginLoaderTest
{
    [Fact]
    public void PluginLoader_CorrectLoading_and_Executing()
    {
        var loader = new PluginLoader(Path.Combine("..", "..", "..", "PluginsForTest"));
        var output = new StringWriter();
        Console.SetOut(output);

        var expectedValue = "Выполнен первый плагин\nВыполнен второй плагин\nВыполнен четвёртый плагин\nВыполнен третий плагин\n";

        loader.Load();

        Assert.Equal(expectedValue, output.ToString());
    }

    [Fact]
    public void PluginLoader_PluginsNotFound_Ex()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);

        var loader = new PluginLoader(testDir);

        Assert.Throws<Exception>(loader.Load).Message.Equals("Плагины не найдены");

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void PluginLoader_DirectoryNotExists_Ex()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");

        var loader = new PluginLoader(testDir);

        Assert.Throws<Exception>(loader.Load).Message.Equals("Неверный путь");
    }
}
