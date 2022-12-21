using System;
using System.Collections.Generic;

namespace School_Labb3.Models
{
    public partial class staff
    {
        public staff()
        {
            StaffAdmins = new HashSet<StaffAdmin>();
        }

        public int StaffId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? HireDate { get; set; }

        public virtual ICollection<StaffAdmin> StaffAdmins { get; set; }
    }
}
