using System.Collections.Concurrent;

namespace task18;

public class ServerThread
{
    private volatile bool _started = true;
    private volatile bool _hardStop;
    private readonly BlockingCollection<ICommand> _commands = [];
    private readonly Scheduler _scheduler = new();
    private readonly Thread _thread;
    private readonly ManualResetEvent _commandAdded = new(false);

    public ServerThread()
    {
        _thread = new Thread(RunICommands);
        _thread.Start();
    }

    public void AddCommand(ICommand command)
    {
        if (!_started) throw new InvalidOperationException("Поток не запущен");
        _commands.Add(command);
        _commandAdded.Set(); 
    }

    public void HardStop()
    {
        if (Thread.CurrentThread != _thread) throw new InvalidOperationException("Команда HardStop успешно выполняюется только в потоке, который она должна остановить");
        _hardStop = true;
        _commands.CompleteAdding();
        _started = false;
        _thread.Interrupt();
    }

    public void SoftStop()
    {
        if (Thread.CurrentThread != _thread) throw new InvalidOperationException("Команда SoftStop успешно выполняюется только в потоке, который она должна остановить");
        _started = false;
        _commands.CompleteAdding();
    }

    public void RunICommands()
    {
        try
        {
            while (_started)
            {
                while (_commands.TryTake(out var newCommand))
                {
                    _scheduler.Add(newCommand);
                }

                if (_scheduler.HasCommand() && !_hardStop)
                {
                    var command = _scheduler.Select();
                    if (command != null)
                    {
                        try { command.Execute(); }
                        catch (Exception ex) { ExceptionHandler.ProccesEx(command, ex); }
                    }
                }
                _commandAdded.Reset();
                if (_commands.Count == 0 && !_scheduler.HasCommand()) _commandAdded.WaitOne(100);
            }

            if (!_hardStop)
            {
                while (_scheduler.HasCommand())
                {
                    var command = _scheduler.Select();
                    if (command != null)
                    {
                        try { command.Execute(); }
                        catch (Exception ex) { ExceptionHandler.ProccesEx(command, ex); }
                    }
                }
            }
        }
        catch (ThreadInterruptedException) { }
        catch (InvalidOperationException) { }
    }
}
