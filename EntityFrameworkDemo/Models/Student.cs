using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDemo.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RollNumber { get; set; }
        public string Class { get; set; }
        public string Section { get; set; } = "NA";

    }
}
