using System;
using System.Collections.Generic;

namespace School_Labb3.Models
{
    public partial class Address
    {
        public Address()
        {
            StaffAdmins = new HashSet<StaffAdmin>();
            Students = new HashSet<Student>();
        }

        public int AddressId { get; set; }
        public string StreetAddress { get; set; } = null!;
        public string PostCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Homeland { get; set; }

        public virtual ICollection<StaffAdmin> StaffAdmins { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
