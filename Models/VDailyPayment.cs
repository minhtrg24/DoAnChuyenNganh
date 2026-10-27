using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class VDailyPayment
    {
        public DateTime? PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public long? PaymentCount { get; set; }
        public decimal? TotalCollected { get; set; }
    }
}
