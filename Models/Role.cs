using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public byte RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
