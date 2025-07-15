using System.Collections.Concurrent;

namespace task17;

public class ServerThread
{
    private volatile bool _started = true;
    private volatile bool _hardStop;
    private readonly BlockingCollection<ICommand> _Commands = [];
    private readonly Thread _thread;

    public ServerThread()
    {
        _thread = new Thread(RunICommands);
        _thread.Start();
    }

    public void AddCommand(ICommand command)
    {
        if (!_started) throw new InvalidOperationException("Поток не запущен");
        _Commands.Add(command);
    }

    public void HardStop()
    {
        if (Thread.CurrentThread != _thread) throw new InvalidOperationException("Команда HardStop успешно выполняюется только в потоке, который она должна остановить");
        _hardStop = true;
        _Commands.CompleteAdding();
        _started = false;
        _thread.Interrupt();
    }

    public void SoftStop()
    {
        if (Thread.CurrentThread != _thread) throw new InvalidOperationException("Команда SoftStop успешно выполняюется только в потоке, который она должна остановить");
        _started = false;
        _Commands.CompleteAdding();
    }

    public void RunICommands()
    {
        try
        {
            while (_started)
            {
                var command = _Commands.Take();
                try { command.Execute(); }
                catch (Exception ex) { ExceptionHandler.ProccesEx(command, ex); }
            }
            if (!_hardStop)
            {
                while (_Commands.TryTake(out var command))
                {
                    try { command.Execute(); }
                    catch (Exception ex) { ExceptionHandler.ProccesEx(command, ex); }
                }
            }
        }
        catch (ThreadInterruptedException) { }
        catch (InvalidOperationException) { }
    }
}   
