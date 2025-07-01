namespace task07;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class VersionAttribute(int major, int minor) : Attribute
{
    public int Major { get; } = major;
    public int Minor { get; } = minor;
}
