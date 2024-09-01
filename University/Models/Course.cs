using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using University.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }

    public string Name { get; set; }

    public ICollection<Student> Students { get; set; }
    public ICollection<Subject> Subjects { get; set; }

}