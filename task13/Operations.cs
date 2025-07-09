using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13;

public class FormatDate : JsonConverter<DateTime>
{
  public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  => DateTime.ParseExact(reader.GetString()!, "dd MM yyyy", null);

  public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
  => writer.WriteStringValue(value.ToString("dd MM yyyy", null));
}

public static class JsonOperations
{
  private static JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

  public static string SerializeSubject(Subject subject)
  => JsonSerializer.Serialize(subject, _options);

  public static Subject? DeserializeSubject(string jsonString)
  => JsonSerializer.Deserialize<Subject>(jsonString, _options);

  public static void FileSerializeSubject(Subject subject, string path)
  => File.WriteAllText(path, SerializeSubject(subject));

  public static Subject? FileDeserializeSubject(string path)
  => DeserializeSubject(File.ReadAllText(path));

  public static string SerializeStudent(Student student)
  => JsonSerializer.Serialize(student, _options);

  public static Student? DeserializeStudent(string jsonString)
  => JsonSerializer.Deserialize<Student>(jsonString, _options);

  public static void FileSerializeStudent(Student student, string path)
  => File.WriteAllText(path, SerializeStudent(student));

  public static Student? FileDeserializeStudent(string path)
  => DeserializeStudent(File.ReadAllText(path));
}
