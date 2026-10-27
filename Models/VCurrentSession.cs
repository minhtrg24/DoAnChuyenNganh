using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class VCurrentSession
    {
        public long SessionId { get; set; }
        public string ComputerCode { get; set; } = null!;
        public string ZoneName { get; set; } = null!;
        public string? MemberCode { get; set; }
        public string? CustomerName { get; set; }
        public string? MembershipTier { get; set; }
        public decimal? TotalDeposited { get; set; }
        public DateTime StartedAt { get; set; }
        public int? ElapsedMinutes { get; set; }
        public decimal HourlyRateSnapshot { get; set; }
        public string Status { get; set; } = null!;
    }
}
