namespace task07tests;

using Xunit;
using task07;
using System.Reflection;

public class AttributeReflectionTests
{
    [Fact]
    public void Class_HasDisplayNameAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Пример класса", attribute.DisplayName);
    }

    [Fact]
    public void Method_HasDisplayNameAttribute()
    {
        var method = typeof(SampleClass).GetMethod("TestMethod");
        var attribute = method?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Тестовый метод", attribute.DisplayName);
    }

    [Fact]
    public void Property_HasDisplayNameAttribute()
    {
        var prop = typeof(SampleClass).GetProperty("Number");
        var attribute = prop?.GetCustomAttribute<DisplayNameAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal("Числовое свойство", attribute.DisplayName);
    }

    [Fact]
    public void Class_HasVersionAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = type.GetCustomAttribute<VersionAttribute>();
        Assert.NotNull(attribute);
        Assert.Equal(1, attribute.Major);
        Assert.Equal(0, attribute.Minor);
    }

    [Fact]
    public void ReflectionHelper_test()
    {
        var helper = new ReflectionHelper(typeof(SampleClass));
        helper.PrintTypeInfo();
    }

    [Fact]
    public void ReflectionHelper_GetClassInfo_return_correct_ClassInfo()
    {
        var type = typeof(SampleClass);
        var helper = new ReflectionHelper(type);
        ClassInfo classInfo = helper.GetClassInfo();
        Assert.Equal(classInfo.NameAtt, type.GetCustomAttribute<DisplayNameAttribute>());
        Assert.Equal(classInfo.VersionAtt, type.GetCustomAttribute<VersionAttribute>());
        Assert.Equal(classInfo.Methods.Select(m => m.Name), ["TestMethod", "TestMethod2", "TestMethodPrivate"]);
        Assert.Equal(classInfo.Properties.Select(p => p.Name), ["Number", "Number2", "Number3"]);
    }
}
