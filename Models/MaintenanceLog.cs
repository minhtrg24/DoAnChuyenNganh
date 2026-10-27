using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class MaintenanceLog
    {
        public long MaintenanceId { get; set; }
        public long ComputerId { get; set; }
        public long? ReportedBy { get; set; }
        public long? AssignedTo { get; set; }
        public string IssueTitle { get; set; } = null!;
        public string? IssueDetail { get; set; }
        public string Status { get; set; } = null!;
        public decimal RepairCost { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string? ResolutionNote { get; set; }

        public virtual Employee? AssignedToNavigation { get; set; }
        public virtual Computer Computer { get; set; } = null!;
        public virtual Employee? ReportedByNavigation { get; set; }
    }
}
