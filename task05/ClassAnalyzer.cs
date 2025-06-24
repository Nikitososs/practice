using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace task05;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
    => _type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName)
            .Select(m => m.Name);

    public IEnumerable<string> GetMethodParams(string methodname)
    {
        var method = _type.GetMethod(methodname);
        if (method == null) return Enumerable.Empty<string>();

        return method.GetParameters()
            .Select(p => p.Name ?? string.Empty);
    }

    public IEnumerable<string> GetAllFields()
    => _type.GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(f => !f.Name.Contains(">k__BackingField"))
            .Select(f => f.Name);


    public IEnumerable<string> GetProperties()
    => _type.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic)
            .Select(p => p.Name);

    public bool HasAttribute<T>() where T : Attribute
    => _type.GetCustomAttributes(typeof(T), false).Any();
}
