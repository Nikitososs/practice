using System;
using System.Linq;
public class Student
{
    public string Name { get; set; }
    public string Faculty { get; set; }
    public List<int> Grades { get; set; }
}

public class StudentService
{
    private readonly List<Student> _students;

    public StudentService(List<Student> students) => _students = students;

    public IEnumerable<Student> GetStudentsByFaculty(string faculty)
    => _students.Where(student => student.Faculty.Equals(faculty));

    public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade)
    => _students.Where(student => student.Grades.Average() >= minAverageGrade);

    public IEnumerable<Student> GetStudentsOrderedByName()
    => _students.OrderBy(student => student.Name);

    public ILookup<string, Student> GroupStudentsByFaculty()
    => _students.ToLookup(student => student.Faculty);

    public string GetFacultyWithHighestAverageGrade()
    => _students.GroupBy(student => student.Faculty)
    .OrderByDescending(g => g.Average(student => student.Grades.Average()))
    .First().Key;
}
