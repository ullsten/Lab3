using System;
using System.Collections.Generic;

namespace School_Labb3.Models
{
    public partial class Course
    {
        public Course()
        {
            Exams = new HashSet<Exam>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
