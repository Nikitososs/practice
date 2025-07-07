namespace task07;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class DisplayNameAttribute(string name) : Attribute
{
    public string DisplayName { get; } = name;
}
