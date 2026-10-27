using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class Payment
    {
        public long PaymentId { get; set; }
        public long? SessionId { get; set; }
        public long? OrderId { get; set; }
        public long? WalletTransactionId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public decimal Amount { get; set; }
        public long ReceivedBy { get; set; }
        public string? ReferenceCode { get; set; }
        public DateTime PaidAt { get; set; }
        public string? Note { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Employee ReceivedByNavigation { get; set; } = null!;
        public virtual GamingSession? Session { get; set; }
        public virtual WalletTransaction? WalletTransaction { get; set; }
    }
}
