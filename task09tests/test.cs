using System.Reflection;
using LibInfoNs;
using ConsoleMetadata;

namespace task09tests;

public class Task09tests
{
    [Fact]
    public void Console_App_Test()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        var expectedValue = String.Join("\n", File.ReadAllLines(Path.Combine("..", "..", "..", "expectedOutput.txt")));

        Program.Main(["task07.dll"]);

        Assert.Equal(expectedValue, output.ToString());
    }

    [Fact]
    public void LibInfo_Returns_Correct_Types()
    {
        Assembly testassembly = Assembly.LoadFrom("LibInfo.dll");
        LibInfo libInfo = new LibInfo(testassembly);
        var info = libInfo.FindedTypes;
        Assert.Equal(["Parameter", "LibConstructor", "LibMethod", "LibType", "LibInfo"], info.Select(t => t.Name));
    }

    [Fact]
    public void LibInfo_Types_Contains_Correct_Attributes()
    {
        Assembly assembly = Assembly.LoadFrom("LibInfo.dll");
        LibInfo libInfo = new LibInfo(assembly);
        var info = libInfo.FindedTypes;
        Assert.Equal([[], [], [], [], ["DisplayNameAttribute", "VersionAttribute"]],
                    info.Select(t => t.Attributes.Select(a => a).ToList()).ToList());
    }

    [Fact]
    public void LibInfo_Types_Contains_Correct_Constructors()
    {
        Assembly assembly = Assembly.LoadFrom("LibInfo.dll");
        LibInfo libInfo = new LibInfo(assembly);
        var info = libInfo.FindedTypes;
        Assert.Equal([["Parameter"], ["LibConstructor"], ["LibMethod"], ["LibType"], ["LibInfo"]],
                    info.Select(t => t.Constructors.Select(c => c.Name).ToList()).ToList());
    }

    [Fact]
    public void LibInfo_Types_Contains_Correct_Methods()
    {
        Assembly assembly = Assembly.LoadFrom("LibInfo.dll");
        LibInfo libInfo = new LibInfo(assembly);
        var info = libInfo.FindedTypes;
        Assert.Equal([["Print"], ["Print"], ["Print"], ["Print"], ["Print"]],
                    info.Select(t => t.Methods.Select(m => m.Name).ToList()).ToList());
    }
}
