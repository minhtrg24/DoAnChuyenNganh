using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class ComputerZone
    {
        public ComputerZone()
        {
            Computers = new HashSet<Computer>();
        }

        public int ZoneId { get; set; }
        public string ZoneName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
    }
}
