using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    [Key]
    public int StudentId { get; set; }
    public string Name { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
}