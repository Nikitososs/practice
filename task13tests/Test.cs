using task13;

namespace task13tests;

public class OperationTest
{
    private static List<Subject> _testSubjects = new() { new Subject("sub1", 5), new Subject("sub2", 5) };
    private static Student _testStudent = new Student("Firstname", "LastName", new DateTime(2025, 7, 10), _testSubjects);

    [Fact]
    public void Subject_Serialization_Equals_Deserialization()
    {
        string serlialized = JsonOperations.SerializeSubject(_testSubjects[0]);
        Subject deserilalized = JsonOperations.DeserializeSubject(serlialized)!;

        Assert.Equal(_testSubjects[0].Grade, deserilalized.Grade);
        Assert.Equal(_testSubjects[0].Name, deserilalized.Name);
    }

    [Fact]
    public void Student_Serialization_Equals_Deserialization()
    {
        string serlialized = JsonOperations.SerializeStudent(_testStudent);
        Student deserilalized = JsonOperations.DeserializeStudent(serlialized)!;

        Assert.Equal(_testStudent.FirstName, deserilalized.FirstName);
        Assert.Equal(_testStudent.LastName, deserilalized.LastName);
        Assert.Equal(_testStudent.BirthDate, deserilalized.BirthDate);
    }

    [Fact]
    public void Student_To_File_Serialization_Equals_Deserialization()
    {
        var path = Path.Combine(Path.GetTempPath(), "test.json");
        JsonOperations.FileSerializeStudent(_testStudent, path);

        Student deserilalized = JsonOperations.FileDeserializeStudent(path)!;

        Assert.Equal(_testStudent.FirstName, deserilalized.FirstName);
        Assert.Equal(_testStudent.LastName, deserilalized.LastName);
        Assert.Equal(_testStudent.BirthDate, deserilalized.BirthDate);

        File.Delete(path);
    }

    [Fact]
    public void Serialization_Ignores_Null()
    {
        Student student = new Student(null, null, new DateTime(2025, 7, 10), _testSubjects);
        string serlialized = JsonOperations.SerializeStudent(student); ;

        Assert.DoesNotContain("FirstName", serlialized);
        Assert.DoesNotContain("LastName", serlialized);
    }

    [Fact]
    public void Serialization_Custom_Datetime()
    {
        string serlialized = JsonOperations.SerializeStudent(_testStudent); ;

        Assert.Contains("10 07 2025", serlialized);
    }
}
