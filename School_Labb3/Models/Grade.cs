using System;
using System.Collections.Generic;

namespace School_Labb3.Models
{
    public partial class Grade
    {
        public Grade()
        {
            Exams = new HashSet<Exam>();
        }

        public int GradeId { get; set; }
        public int GradeName { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
