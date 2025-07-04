using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    private string _path;

    public DirectorySizeCommand(string path)
    {
        _path = path;
    }
    public long? Length { get; private set; }
    public void Execute()
    {
        if (!Directory.Exists(_path)) return;
        Length = RecursiveSize(_path);
    }
    private long RecursiveSize(string path)
    {
        long size = 0;
        DirectoryInfo di = new DirectoryInfo(@$"{path}");
        size += di.GetFiles().Select(f => f.Length).Sum();
        size += di.GetDirectories().Select(d => RecursiveSize(d.FullName)).Sum();
        return size;
    }
}
