using CommandLib;

namespace FileSystemCommands;

public class FindFilesCommand : ICommand
{
    private string _directory;
    private string _mask;

    public FindFilesCommand(string directory, string mask)
    {
        _directory = directory;
        _mask = mask;
    }
    public FileInfo[]? Files { get; private set; }
    public void Execute()
    {
        if (!Directory.Exists(_directory)) return;
        DirectoryInfo di = new DirectoryInfo(@$"{_directory}");
        Files = di.GetFiles(_mask);
    }
}
