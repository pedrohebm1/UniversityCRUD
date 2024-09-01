using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public List<Student> Courses { get; set; }

    }
}
