using System.Collections.Concurrent;

namespace task18;

public class Scheduler : IScheduler
{
    private readonly ConcurrentQueue<ICommand> _commands = new();
    private readonly ConcurrentQueue<ICommandLong> _longCommnads = new();
    private int _nextCommand = 1;

    public void Add(ICommand command)
    {
        if (command is ICommandLong) _longCommnads.Enqueue((ICommandLong)command);
        else _commands.Enqueue(command);
    }

    public bool HasCommand() => !_commands.IsEmpty || !_longCommnads.IsEmpty;

    public ICommand? Select()
    {
        _nextCommand = 1 - _nextCommand;

        if (_nextCommand == 0 && _commands.TryDequeue(out var command)) return command;

        if (_nextCommand == 1 && _longCommnads.TryDequeue(out var longCommand))
        {
            if (longCommand.ExecutionsToComplete > 1)
            {
                _longCommnads.Enqueue(longCommand);
            }
            return longCommand;
        }

        if (_nextCommand == 1 && _commands.TryDequeue(out command)) return command;

        if (_nextCommand == 0 && _longCommnads.TryDequeue(out longCommand))
        {
            if (longCommand.ExecutionsToComplete > 1)
            {
                _longCommnads.Enqueue(longCommand);
            }
            return longCommand;
        }
        return null;
    }
}
