namespace task10;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class PluginLoad(Type[] dependences) : Attribute
{
    public Type[] Dependences { get; } = dependences;
}