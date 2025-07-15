namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
    {
        double result = 0.0;
        double segment = (b - a) / threadsNumber;

        Barrier barrier = new Barrier(threadsNumber + 1);
        List<Thread> threads = Enumerable.Range(0, threadsNumber)
            .Select(i =>
            {
                double a_i = a + i * segment;
                double b_i = (i == threadsNumber - 1) ? b : a_i + segment;

                Thread thread = new Thread(() =>
                {
                    double threadRes = SolveSingleThread(a_i, b_i, function, step);
                    UseInterlocked(threadRes, ref result);
                    barrier.SignalAndWait();
                });
                thread.Start();
                return thread;
            })
            .ToList();

        barrier.SignalAndWait();
        barrier.Dispose();
        return result;
    }

    public static double SolveSingleThread(double a, double b, Func<double, double> function, double step)
    {
        int n = Math.Max(1, (int)Math.Ceiling((b - a) / step));
        double h = (b - a) / n;

        return Enumerable.Range(0, n)
            .Select(i => h * (function(a + i * h) + function(a + (i + 1) * h)) / 2)
            .Sum();
    }

    private static void UseInterlocked(double threadRes, ref double result)
    {
        double res = result;
        double nRes = res + threadRes;
        if (Interlocked.CompareExchange(ref result, nRes, res) != res) UseInterlocked(threadRes, ref result);
    }
}
