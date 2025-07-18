using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11;

public class Generator
{
    public static dynamic GenerateClass(string code, string className)
    {
        using MemoryStream output = new();

        CSharpCompilation.Create(
            "Generated",
            [CSharpSyntaxTree.ParseText(code)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .Emit(output);

        return Activator.CreateInstance(Assembly.Load(output.ToArray()).GetType(className)!)!;
    }
}
