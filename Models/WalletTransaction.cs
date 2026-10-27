using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class WalletTransaction
    {
        public long WalletTransactionId { get; set; }
        public long CustomerId { get; set; }
        public long? EmployeeId { get; set; }
        public string TxnType { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public string? ReferenceType { get; set; }
        public long? ReferenceId { get; set; }
        public string? Note { get; set; }
        public DateTime TxnAt { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Employee? Employee { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
