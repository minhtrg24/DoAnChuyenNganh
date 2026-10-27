using System;
using System.Collections.Generic;

namespace DoAnChuyenNganh.Models
{
    public partial class Employee
    {
        public Employee()
        {
            GamingSessionEndedByNavigations = new HashSet<GamingSession>();
            GamingSessionStartedByNavigations = new HashSet<GamingSession>();
            MaintenanceLogAssignedToNavigations = new HashSet<MaintenanceLog>();
            MaintenanceLogReportedByNavigations = new HashSet<MaintenanceLog>();
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
            WalletTransactions = new HashSet<WalletTransaction>();
        }

        public long EmployeeId { get; set; }
        public byte RoleId { get; set; }
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Phone { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<GamingSession> GamingSessionEndedByNavigations { get; set; }
        public virtual ICollection<GamingSession> GamingSessionStartedByNavigations { get; set; }
        public virtual ICollection<MaintenanceLog> MaintenanceLogAssignedToNavigations { get; set; }
        public virtual ICollection<MaintenanceLog> MaintenanceLogReportedByNavigations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<WalletTransaction> WalletTransactions { get; set; }
    }
}
