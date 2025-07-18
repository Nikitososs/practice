using task11;

namespace task11tests;

public class GeneratorTests
{

    private string _code = @"
            public class Calculator
                {
                    public int Add(int a, int b) => a + b;
                    public int Minus(int a, int b) => a - b;
                    public int Mul(int a, int b) => a * b;
                    public int Div(int a, int b) => a / b;
                }";

    [Fact]
    public void Add_Returns_Correct()
    {
        dynamic calc = Generator.GenerateClass(_code, "Calculator");
        Assert.Equal(2, calc.Add(1, 1));
    }

    [Fact]
    public void Minus_Returns_Correct()
    {
        dynamic calc = Generator.GenerateClass(_code, "Calculator");
        Assert.Equal(0, calc.Minus(1, 1));
    }

    [Fact]
    public void Mul_Returns_Correct()
    {
        dynamic calc = Generator.GenerateClass(_code, "Calculator");
        Assert.Equal(1, calc.Mul(1, 1));
    }

    [Fact]
    public void Div_Returns_Correct()
    {
        dynamic calc = Generator.GenerateClass(_code, "Calculator");
        Assert.Equal(2, calc.Div(4, 2));
    }

    [Fact]
    public void Div_By_Zero()
    {
        dynamic calc = Generator.GenerateClass(_code, "Calculator");
        Assert.Throws<DivideByZeroException>(() => calc.Div(777, 0));
    }
}
