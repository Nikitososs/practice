[TestFixture]
public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    [SetUp]
    public void Setup()
    {
        _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
        };
        _service = new StudentService(_testStudents);
    }

    [Test]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(s => s.Faculty == "ФИТ"));
    }

    [Test]
    public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsWithMinAverageGrade(4).ToList();
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(s => s.Name == "Иван"));
        Assert.IsTrue(result.Any(s => s.Name == "Петр"));
    }

    [Test]
    public void GetStudentsOrderedByName_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsOrderedByName().ToList();
        Assert.AreEqual("Анна", result[0].Name);
        Assert.AreEqual("Иван", result[1].Name);
        Assert.AreEqual("Петр", result[2].Name);
    }

    [Test]
    public void GroupStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GroupStudentsByFaculty();
        Assert.AreEqual(2, result.Count);
        
        var fitStudents = result["ФИТ"].ToList();
        Assert.AreEqual(2, fitStudents.Count);
        Assert.IsTrue(fitStudents.Any(s => s.Name == "Иван"));
        Assert.IsTrue(fitStudents.Any(s => s.Name == "Анна"));
        
        var economyStudents = result["Экономика"].ToList();
        Assert.AreEqual(1, economyStudents.Count);
        Assert.IsTrue(economyStudents.Any(s => s.Name == "Петр"));
    }

    [Test]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.AreEqual("Экономика", result);
    }
}