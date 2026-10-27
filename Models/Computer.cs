using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class Computer
    {
        public Computer()
        {
            MaintenanceLogs = new HashSet<MaintenanceLog>();
        }

        public long ComputerId { get; set; }
        public int ZoneId { get; set; }
        public string ComputerCode { get; set; } = null!;
        public string ComputerType { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public string Status { get; set; } = null!;
        public string? IpAddress { get; set; }
        public string? Specifications { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ComputerZone Zone { get; set; } = null!;
        public virtual GamingSession GamingSession { get; set; } = null!;
        public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; }
    }
}
