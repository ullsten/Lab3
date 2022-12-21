using System;
using System.Collections.Generic;

namespace School_Labb3.Models
{
    public partial class Position
    {
        public Position()
        {
            StaffAdmins = new HashSet<StaffAdmin>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; } = null!;

        public virtual ICollection<StaffAdmin> StaffAdmins { get; set; }
    }
}
