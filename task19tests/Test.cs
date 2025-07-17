using task19;

namespace task19tests;

public class LongCommandsTest
{
    [Fact]
    public void CorrectExecutionsTest()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        var expectedValue = File.ReadAllText(Path.Combine("..", "..", "..", "expectedOutput.txt"));

        Program.Main();

        Assert.Equal(expectedValue, output.ToString());
    }
}
