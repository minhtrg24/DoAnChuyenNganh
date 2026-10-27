using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoAnChuyenNganh.Models
{
    public partial class tiem_netContext : DbContext
    {
        public tiem_netContext()
        {
        }

        public tiem_netContext(DbContextOptions<tiem_netContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Computer> Computers { get; set; } = null!;
        public virtual DbSet<ComputerZone> ComputerZones { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<GamingSession> GamingSessions { get; set; } = null!;
        public virtual DbSet<MaintenanceLog> MaintenanceLogs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<VCurrentSession> VCurrentSessions { get; set; } = null!;
        public virtual DbSet<VDailyPayment> VDailyPayments { get; set; } = null!;
        public virtual DbSet<WalletTransaction> WalletTransactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=tiem_net;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.ToTable("computers");

                entity.HasIndex(e => e.Status, "IX_computers_status");

                entity.HasIndex(e => new { e.ZoneId, e.Status }, "IX_computers_zone_status");

                entity.HasIndex(e => e.ComputerCode, "UQ_computers_computer_code")
                    .IsUnique();

                entity.HasIndex(e => e.IpAddress, "UX_computers_ip_not_null")
                    .IsUnique()
                    .HasFilter("([ip_address] IS NOT NULL)");

                entity.Property(e => e.ComputerId).HasColumnName("computer_id");

                entity.Property(e => e.ComputerCode)
                    .HasMaxLength(30)
                    .HasColumnName("computer_code");

                entity.Property(e => e.ComputerType)
                    .HasMaxLength(20)
                    .HasColumnName("computer_type")
                    .HasDefaultValueSql("(N'STANDARD')");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.HourlyRate)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("hourly_rate");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(45)
                    .HasColumnName("ip_address");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .HasColumnName("notes");

                entity.Property(e => e.Specifications)
                    .HasMaxLength(500)
                    .HasColumnName("specifications");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("(N'AVAILABLE')");

                entity.Property(e => e.UpdatedAt)
                    .HasPrecision(0)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ZoneId).HasColumnName("zone_id");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_computers_zone");
            });

            modelBuilder.Entity<ComputerZone>(entity =>
            {
                entity.HasKey(e => e.ZoneId);

                entity.ToTable("computer_zones");

                entity.HasIndex(e => e.ZoneName, "UQ_computer_zones_zone_name")
                    .IsUnique();

                entity.Property(e => e.ZoneId).HasColumnName("zone_id");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.ZoneName)
                    .HasMaxLength(80)
                    .HasColumnName("zone_name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.FullName, "IX_customers_name");

                entity.HasIndex(e => e.MemberCode, "UQ_customers_member_code")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UX_customers_phone_not_null")
                    .IsUnique()
                    .HasFilter("([phone] IS NOT NULL)");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(120)
                    .HasColumnName("full_name");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MemberCode)
                    .HasMaxLength(30)
                    .HasColumnName("member_code");

                entity.Property(e => e.MembershipTier)
                    .HasMaxLength(10)
                    .HasColumnName("membership_tier")
                    .HasDefaultValueSql("(N'NORMAL')");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.TotalDeposited)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("total_deposited")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.UpdatedAt)
                    .HasPrecision(0)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.WalletBalance)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("wallet_balance")
                    .HasDefaultValueSql("((0.00))");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.FullName, "IX_employees_name");

                entity.HasIndex(e => e.Username, "UQ_employees_username")
                    .IsUnique();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.FullName)
                    .HasMaxLength(120)
                    .HasColumnName("full_name");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasColumnName("password_hash");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdatedAt)
                    .HasPrecision(0)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Username)
                    .HasMaxLength(60)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employees_role");
            });

            modelBuilder.Entity<GamingSession>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.ToTable("gaming_sessions");

                entity.HasIndex(e => new { e.CustomerId, e.StartedAt }, "IX_sessions_customer_time");

                entity.HasIndex(e => new { e.Status, e.StartedAt }, "IX_sessions_status_time");

                entity.HasIndex(e => e.ComputerId, "UX_one_active_session_per_computer")
                    .IsUnique()
                    .HasFilter("([status]=N'PLAYING')");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.ComputerId).HasColumnName("computer_id");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.EndedAt)
                    .HasPrecision(0)
                    .HasColumnName("ended_at");

                entity.Property(e => e.EndedBy).HasColumnName("ended_by");

                entity.Property(e => e.HourlyRateSnapshot)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("hourly_rate_snapshot");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .HasColumnName("notes");

                entity.Property(e => e.StartedAt)
                    .HasPrecision(0)
                    .HasColumnName("started_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.StartedBy).HasColumnName("started_by");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .HasColumnName("status")
                    .HasDefaultValueSql("(N'PLAYING')");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("total_amount")
                    .HasDefaultValueSql("((0.00))");

                entity.HasOne(d => d.Computer)
                    .WithOne(p => p.GamingSession)
                    .HasForeignKey<GamingSession>(d => d.ComputerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sessions_computer");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.GamingSessions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_sessions_customer");

                entity.HasOne(d => d.EndedByNavigation)
                    .WithMany(p => p.GamingSessionEndedByNavigations)
                    .HasForeignKey(d => d.EndedBy)
                    .HasConstraintName("FK_sessions_ended_by");

                entity.HasOne(d => d.StartedByNavigation)
                    .WithMany(p => p.GamingSessionStartedByNavigations)
                    .HasForeignKey(d => d.StartedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sessions_started_by");
            });

            modelBuilder.Entity<MaintenanceLog>(entity =>
            {
                entity.HasKey(e => e.MaintenanceId);

                entity.ToTable("maintenance_logs");

                entity.HasIndex(e => new { e.ComputerId, e.Status }, "IX_maintenance_computer_status");

                entity.HasIndex(e => e.OpenedAt, "IX_maintenance_opened");

                entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");

                entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");

                entity.Property(e => e.ComputerId).HasColumnName("computer_id");

                entity.Property(e => e.IssueDetail).HasColumnName("issue_detail");

                entity.Property(e => e.IssueTitle)
                    .HasMaxLength(160)
                    .HasColumnName("issue_title");

                entity.Property(e => e.OpenedAt)
                    .HasPrecision(0)
                    .HasColumnName("opened_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RepairCost)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("repair_cost")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.ReportedBy).HasColumnName("reported_by");

                entity.Property(e => e.ResolutionNote)
                    .HasMaxLength(500)
                    .HasColumnName("resolution_note");

                entity.Property(e => e.ResolvedAt)
                    .HasPrecision(0)
                    .HasColumnName("resolved_at");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("(N'OPEN')");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.MaintenanceLogAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .HasConstraintName("FK_maintenance_assignee");

                entity.HasOne(d => d.Computer)
                    .WithMany(p => p.MaintenanceLogs)
                    .HasForeignKey(d => d.ComputerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_maintenance_computer");

                entity.HasOne(d => d.ReportedByNavigation)
                    .WithMany(p => p.MaintenanceLogReportedByNavigations)
                    .HasForeignKey(d => d.ReportedBy)
                    .HasConstraintName("FK_maintenance_reporter");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.SessionId, "IX_orders_session");

                entity.HasIndex(e => new { e.OrderedAt, e.Status }, "IX_orders_time_status");

                entity.HasIndex(e => e.OrderCode, "UQ_orders_order_code")
                    .IsUnique();

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .HasColumnName("note");

                entity.Property(e => e.OrderCode)
                    .HasMaxLength(30)
                    .HasColumnName("order_code");

                entity.Property(e => e.OrderedAt)
                    .HasPrecision(0)
                    .HasColumnName("ordered_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.OrderedBy).HasColumnName("ordered_by");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .HasColumnName("status")
                    .HasDefaultValueSql("(N'NEW')");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("total_amount")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.UpdatedAt)
                    .HasPrecision(0)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_orders_customer");

                entity.HasOne(d => d.OrderedByNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_orders_employee");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_orders_session");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");

                entity.HasIndex(e => e.OrderId, "IX_order_items_order");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.LineTotal)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("line_total")
                    .HasComputedColumnSql("(CONVERT([decimal](12,2),[quantity]*[unit_price]))", true);

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductNameSnapshot)
                    .HasMaxLength(120)
                    .HasColumnName("product_name_snapshot");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_order_items_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_order_items_product");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.HasIndex(e => e.OrderId, "IX_payments_order");

                entity.HasIndex(e => e.SessionId, "IX_payments_session");

                entity.HasIndex(e => e.PaidAt, "IX_payments_time");

                entity.HasIndex(e => e.WalletTransactionId, "UX_payments_wallet_transaction_not_null")
                    .IsUnique()
                    .HasFilter("([wallet_transaction_id] IS NOT NULL)");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .HasColumnName("note");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaidAt)
                    .HasPrecision(0)
                    .HasColumnName("paid_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(20)
                    .HasColumnName("payment_method");

                entity.Property(e => e.ReceivedBy).HasColumnName("received_by");

                entity.Property(e => e.ReferenceCode)
                    .HasMaxLength(100)
                    .HasColumnName("reference_code");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.WalletTransactionId).HasColumnName("wallet_transaction_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_payments_order");

                entity.HasOne(d => d.ReceivedByNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ReceivedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payments_employee");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_payments_session");

                entity.HasOne(d => d.WalletTransaction)
                    .WithOne(p => p.Payment)
                    .HasForeignKey<Payment>(d => d.WalletTransactionId)
                    .HasConstraintName("FK_payments_wallet_txn");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.Category, "IX_products_category");

                entity.HasIndex(e => e.ProductName, "IX_products_name");

                entity.HasIndex(e => e.ProductCode, "UQ_products_product_code")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(60)
                    .HasColumnName("category")
                    .HasDefaultValueSql("(N'Khác')");

                entity.Property(e => e.CostPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("cost_price")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(30)
                    .HasColumnName("product_code");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(120)
                    .HasColumnName("product_name");

                entity.Property(e => e.SellingPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("selling_price");

                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

                entity.Property(e => e.Unit)
                    .HasMaxLength(20)
                    .HasColumnName("unit")
                    .HasDefaultValueSql("(N'cái')");

                entity.Property(e => e.UpdatedAt)
                    .HasPrecision(0)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => e.RoleName, "UQ_roles_role_name")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("role_id");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<VCurrentSession>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_current_sessions");

                entity.Property(e => e.ComputerCode)
                    .HasMaxLength(30)
                    .HasColumnName("computer_code");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(120)
                    .HasColumnName("customer_name");

                entity.Property(e => e.ElapsedMinutes).HasColumnName("elapsed_minutes");

                entity.Property(e => e.HourlyRateSnapshot)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("hourly_rate_snapshot");

                entity.Property(e => e.MemberCode)
                    .HasMaxLength(30)
                    .HasColumnName("member_code");

                entity.Property(e => e.MembershipTier)
                    .HasMaxLength(10)
                    .HasColumnName("membership_tier");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.StartedAt)
                    .HasPrecision(0)
                    .HasColumnName("started_at");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .HasColumnName("status");

                entity.Property(e => e.TotalDeposited)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("total_deposited");

                entity.Property(e => e.ZoneName)
                    .HasMaxLength(80)
                    .HasColumnName("zone_name");
            });

            modelBuilder.Entity<VDailyPayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_daily_payments");

                entity.Property(e => e.PaymentCount).HasColumnName("payment_count");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(20)
                    .HasColumnName("payment_method");

                entity.Property(e => e.TotalCollected)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("total_collected");
            });

            modelBuilder.Entity<WalletTransaction>(entity =>
            {
                entity.ToTable("wallet_transactions");

                entity.HasIndex(e => new { e.CustomerId, e.TxnAt }, "IX_wallet_customer_time");

                entity.HasIndex(e => new { e.ReferenceType, e.ReferenceId }, "IX_wallet_reference");

                entity.Property(e => e.WalletTransactionId).HasColumnName("wallet_transaction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.BalanceAfter)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("balance_after");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .HasColumnName("note");

                entity.Property(e => e.ReferenceId).HasColumnName("reference_id");

                entity.Property(e => e.ReferenceType)
                    .HasMaxLength(30)
                    .HasColumnName("reference_type");

                entity.Property(e => e.TxnAt)
                    .HasPrecision(0)
                    .HasColumnName("txn_at")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.TxnType)
                    .HasMaxLength(20)
                    .HasColumnName("txn_type");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.WalletTransactions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_wallet_customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WalletTransactions)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_wallet_employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
