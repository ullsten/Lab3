using System;
using System.Collections.Generic;

namespace School_Labb3.Models
{
    public partial class StaffAdmin
    {
        public StaffAdmin()
        {
            Exams = new HashSet<Exam>();
        }

        public int StaffAdminId { get; set; }
        public int? FkStaffId { get; set; }
        public int? FkPositionId { get; set; }
        public int? FkAddressId { get; set; }

        public virtual Address? FkAddress { get; set; }
        public virtual Position? FkPosition { get; set; }
        public virtual staff? FkStaff { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
