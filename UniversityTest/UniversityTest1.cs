using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Controllers;
using University.Models;
using Xunit;

public class CoursesControllerTests
{
    private readonly UniversityContext _context;
    private readonly CoursesController _controller;

    public CoursesControllerTests()
    {
        var options = new DbContextOptionsBuilder<UniversityContext>()
            .UseInMemoryDatabase(databaseName: "UniversityTest")
            .Options;
        _context = new UniversityContext(options);
        _controller = new CoursesController(_context);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfCourses()
    {
        _context.Courses.Add(new Course { CourseId = 1, Name = "SI" });
        _context.Courses.Add(new Course { CourseId = 2, Name = "Design" });
        _context.SaveChanges();

        var result = await _controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Course>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithCourse()
    {
        var course = new Course { CourseId = 3, Name = "Engenharia" };
        _context.Courses.Add(course);
        _context.SaveChanges();

        var result = await _controller.Details(3);

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Course>(viewResult.ViewData.Model);
        Assert.Equal("Engenharia", model.Name);
    }

}

public class StudentsControllerTests
{
    private readonly UniversityContext _context;
    private readonly StudentsController _controller;

    public StudentsControllerTests()
    {
        var options = new DbContextOptionsBuilder<UniversityContext>()
            .UseInMemoryDatabase(databaseName: "UniversityTest")
            .Options;
        _context = new UniversityContext(options);
        _controller = new StudentsController(_context);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfStudents()
    {
        var course1 = new Course { CourseId = 2, Name = "Design" };
        var course2 = new Course { CourseId = 3, Name = "Nutricao" };

        _context.Students.Add(new Student { StudentId = 1, Name = "João", City = "Quatis", Course=course1, CourseId=2, Semester=1});
        _context.Students.Add(new Student { StudentId = 2, Name = "Lucas", City = "VR", Course=course2, CourseId=3, Semester=2 });
        _context.SaveChanges();

        var result = await _controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithStudent()
    {
        var course = new Course { CourseId = 4, Name = "SI" };
        var student = new Student { StudentId = 3, Name = "Lara", City = "Resende", Semester =1, Course=course, CourseId=1 };
        _context.Students.Add(student);
        _context.SaveChanges();

        var result = await _controller.Details(3);

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Student>(viewResult.ViewData.Model);
        Assert.Equal("Lara", model.Name);
    }

}
