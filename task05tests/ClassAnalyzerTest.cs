using Xunit;
using task05;

public class TestClass
{
    public int PublicField;
    private string _privateField = string.Empty;

    public int Property { get; set; }
    private int PropertyPrivate { get; set; }

    public void Method() { }
    public void Method2(int a, int b) { }
    public void Method3(int a, int b, string c, bool d, double e) { }
    private void PrivateMethod() { }

}

[Serializable]
public class AttributedClass { }


public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods().ToList();

        Assert.Contains("Method", methods);
        Assert.Contains("Method2", methods);
        Assert.Contains("Method3", methods);
        Assert.DoesNotContain("PrivateMethod", methods);
        Assert.Equal(3, methods.Count);
    }

    [Fact]
    public void GetMethodParams_ReturnsCorrectParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methodsParams = analyzer.GetMethodParams("Method2").ToList();
        Assert.Equal(2, methodsParams.Count);
        Assert.Contains("a", methodsParams);
        Assert.Contains("b", methodsParams);
    }

    [Fact]
    public void GetMethodParams_IncorrectName()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methodsParams = analyzer.GetMethodParams("Method777").ToList();
        Assert.Empty(methodsParams);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields().ToList();

        Assert.Equal(2, fields.Count);

        Assert.Contains("PublicField", fields);
        Assert.Contains("_privateField", fields);

        Assert.DoesNotContain("<Property>k__BackingField", fields);
        Assert.DoesNotContain("<PropertyPrivate>k__BackingField", fields);
        Assert.DoesNotContain("Property", fields);
        Assert.DoesNotContain("PropertyPrivate", fields);
        Assert.DoesNotContain("Method", fields);
        Assert.DoesNotContain("PrivateMethod", fields);
    }

    [Fact]
    public void GetProperties_ReturnsCorrectProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var properties = analyzer.GetProperties().ToList();
        Assert.Equal(2, properties.Count);

        Assert.Contains("Property", properties);
        Assert.Contains("PropertyPrivate", properties);
    }

    [Fact]
    public void HasAttribute_False()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        Assert.False(analyzer.HasAttribute<SerializableAttribute>());
    }

    [Fact]
    public void HasAttribute_True()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));
        Assert.True(analyzer.HasAttribute<SerializableAttribute>());
    }
}
