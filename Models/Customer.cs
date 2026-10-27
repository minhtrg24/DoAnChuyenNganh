using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class Customer
    {
        public Customer()
        {
            GamingSessions = new HashSet<GamingSession>();
            Orders = new HashSet<Order>();
            WalletTransactions = new HashSet<WalletTransaction>();
        }

        public long CustomerId { get; set; }
        public string MemberCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public decimal WalletBalance { get; set; }
        public decimal TotalDeposited { get; set; }
        public string MembershipTier { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<GamingSession> GamingSessions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WalletTransaction> WalletTransactions { get; set; }
    }
}
