using System;
using System.Collections.Generic;

namespace School_Labb3.Models
{
    public partial class Student
    {
        public Student()
        {
            Exams = new HashSet<Exam>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string DayOfBirth { get; set; } = null!;
        public int? Age { get; set; }
        public string? SecurityNumber { get; set; }
        public string? Gender { get; set; }
        public int? FkAddressId { get; set; }
        public int? FkClassId { get; set; }

        public virtual Address? FkAddress { get; set; }
        public virtual Class? FkClass { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
