using task07;

[DisplayNameAttribute("Пример класса")]
[VersionAttribute(1, 0)]
public class SampleClass
{
    [DisplayNameAttribute("Числовое свойство")]
    public int Number { get; set; }

    [DisplayNameAttribute("Числовое свойство 2")]
    public int Number2 { get; set; }

    private int Number3 { get; set; }

    [DisplayNameAttribute("Тестовый метод")]
    public void TestMethod() { }

    public void TestMethod2(int a, string b) { }

    [DisplayNameAttribute("Тестовый метод, приватный")]
    private void TestMethodPrivate() { }
}
