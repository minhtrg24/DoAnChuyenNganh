using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class GamingSession
    {
        public GamingSession()
        {
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
        }

        public long SessionId { get; set; }
        public long ComputerId { get; set; }
        public long? CustomerId { get; set; }
        public long StartedBy { get; set; }
        public long? EndedBy { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public decimal HourlyRateSnapshot { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Computer Computer { get; set; } = null!;
        public virtual Customer? Customer { get; set; }
        public virtual Employee? EndedByNavigation { get; set; }
        public virtual Employee StartedByNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
