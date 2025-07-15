using System.Diagnostics;
using ScottPlot;
namespace task14;

public class TestingOptimalParams
{
    public static double FindOptimalStepsize()
    {
        var SIN = (double x) => Math.Sin(x);
        double refer = 0.13768113;

        double[] stepSizes = [1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6];
        return stepSizes
            .Where(stepSize => Math.Abs(DefiniteIntegral.SolveSingleThread(0, 100, SIN, stepSize) - refer) < 1e-4)
            .Max();
    }

    public static void GraphForOptimalStepSize()
    {
        var SIN = (double x) => Math.Sin(x);
        double refer = 0.13768113;
        double[] stepSizes = [1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6];
        double[] results = stepSizes.Select(s => DefiniteIntegral.SolveSingleThread(0, 100, SIN, s)).ToArray();

        var graph = new Plot();
        graph.Title("График вычислений");
        graph.XLabel("Результат вычисления");
        graph.YLabel("Размер шага");
        double[] filer = [0, 1, 2, 3, 4, 5];

        graph.Add.Scatter(results, filer).LineWidth = 0;
        graph.Add.VerticalLine(refer - 1e-4, 1, Colors.Green).LegendText = "Зона погрешности 1e-4";
        graph.Add.VerticalLine(refer + 1e-4, 1, Colors.Green);

        graph.Axes.Left.SetTicks(filer, ["1e-1", "1e-2", "1e-3", "1e-4", "1e-5", "1e-6"]);

        graph.SavePng(Path.Combine("..", "..", "..", "..", "StepSizes.png"), 800, 600);
    }

    public static void GraphForThreadConut()
    {
        List<MultithreadRes> multiThreadTestResults = Enumerable
            .Range(1, 12)
            .Select(i => AverageMultiThread(i))
            .ToList();

        double singleThreadResults = AverageSingleThread();

        double[] threadCounts = multiThreadTestResults.Select(x => (double)x.ThreadCount).ToArray();
        double[] executionTimes = multiThreadTestResults.Select(x => x.Time).ToArray();

        Plot graph = new Plot();
        graph.Title("График производительности функкции");
        graph.YLabel("Количество потоков");
        graph.XLabel("Время вычисления функции Solve (мс)");

        graph.Add.VerticalLine(singleThreadResults, 1, Colors.Red).LegendText = "Однопоточная версия";
        graph.Add.Scatter(executionTimes, threadCounts);

        graph.SavePng(Path.Combine("..", "..", "..", "..", "ThreadPerformance.png"), 800, 600);


        MultithreadRes best = multiThreadTestResults
            .Where(r => r.Time == multiThreadTestResults.Min(r => r.Time))
            .First();

        File.WriteAllText(Path.Combine("..", "..", "..", "..", "compare.txt"),
            String.Concat($"Время выполнения однопоточной версии: {singleThreadResults:F2} мс \n",
                "Многопоточная версия:\n",
                $"Оптимальное число потоков: {best.ThreadCount}\n",
                $"Время выполнения с оптимальным количеством потоков: {best.Time:F2} мс\n",
                $"Производительность многопоточной версии относительно однопоточной (в процентах):  {(((singleThreadResults / best.Time) - 1) * 100):F2}%"
        ));
    }

    private static double AverageSingleThread()
    {
        double minStepsize = FindOptimalStepsize();
        var SIN = (double x) => Math.Sin(x);
        return Enumerable.Range(0, 5)
            .Select(i =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                DefiniteIntegral.SolveSingleThread(-100, 100, SIN, minStepsize);
                sw.Stop();
                return sw.Elapsed.TotalMilliseconds;
            })
            .Average();
    }

    private static MultithreadRes AverageMultiThread(int threadCount)
    {
        double minStepsize = FindOptimalStepsize();
        var SIN = (double x) => Math.Sin(x);
        return new MultithreadRes(threadCount, Enumerable.Range(0, 5)
            .Select(i =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                DefiniteIntegral.Solve(-100, 100, SIN, minStepsize, threadCount);
                sw.Stop();
                return sw.Elapsed.TotalMilliseconds;
            })
            .Average());
    }
}

public class MultithreadRes(int threadCount, double time)
{
    public int ThreadCount { get; } = threadCount;
    public double Time { get; } = time;
}
