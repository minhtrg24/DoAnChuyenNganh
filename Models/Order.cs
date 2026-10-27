using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public long OrderId { get; set; }
        public string OrderCode { get; set; } = null!;
        public long? CustomerId { get; set; }
        public long? SessionId { get; set; }
        public long OrderedBy { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public DateTime OrderedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee OrderedByNavigation { get; set; } = null!;
        public virtual GamingSession? Session { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
