using System.Text.Json.Serialization;

namespace task13;

public class Student(string? firstName, string? lastName, DateTime birthDate, List<Subject> grades)
{
    public string? FirstName { get; set; } = firstName;
    public string? LastName { get; set; } = lastName;

    [JsonConverter(typeof(FormatDate))]
    public DateTime BirthDate { get; set; } = birthDate;
    public List<Subject> Grades { get; set; } = grades;
}
