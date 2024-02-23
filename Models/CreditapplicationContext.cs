using Microsoft.EntityFrameworkCore;
using System;

namespace CreditApplicationMVCProject.Models
{
    public partial class CreditapplicationContext : DbContext
    {
        public CreditapplicationContext()
        {
        }

        public CreditapplicationContext(DbContextOptions<CreditapplicationContext> options)
            : base(options)
        {
        }

        public DbSet<PurposeMaster> PurposeMasters { get; set; }
        public DbSet<CreditApplicationStatusMaster> CreditApplicationStatusMasters { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CreditApplication> CreditApplications { get; set; }
        public virtual DbSet<CreditDecision> CreditDecisions { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FinancialInformation> FinancialInformations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Your connection string configuration
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-L573J2R9\\SQLEXPRESS;Database=creditapplication;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationID).HasName("PK__CreditAp__C93A4F794DE3C87E");

                entity.ToTable("CreditApplication");

                entity.Property(e => e.ApplicationID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ApplicationID");

                entity.Property(e => e.RequestedAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CreditApplications)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__CreditApp__Custo__4BAC3F29");

                entity.HasOne(d => d.PurposeMasters)
                    .WithMany(p => p.CreditApplications)
                    .HasForeignKey(d => d.PurposeID)
                    .HasConstraintName("FK__CreditApp__Purpo__4F7CD00D");

                entity.HasOne(d => d.CreditApplicationStatusMasters)
                    .WithMany(p => p.CreditApplications)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK__CreditApp__Statu__4E88AB92");
            });

            // ... (Previous code)

            modelBuilder.Entity<CreditDecision>(entity =>
            {
                entity.HasKey(e => e.DecisionId).HasName("PK__CreditDe__C0F289660890CF43");

                entity.ToTable("CreditDecision");

                entity.Property(e => e.DecisionId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DecisionID");

                entity.Property(e => e.DecisionDetails)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.CreditDecisions)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK__CreditDec__Appli__5165187F");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8E26857BA");

                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FinancialInformation>(entity =>
            {
                entity.HasKey(e => e.FinancialInformationId).HasName("PK__Financia__713EF851616ACAC8");

                entity.ToTable("FinancialInformation");

                entity.Property(e => e.FinancialInformationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FinancialInformationID");

                entity.Property(e => e.Expenses).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MonthlyIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FinancialInformations)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Financial__Custo__4E88ABD4");

                entity.HasOne(d => d.EmploymentStatusMasters)
                    .WithMany(p => p.financialInformations)
                    .HasForeignKey(d => d.EmploymentStatusID)
                    .HasConstraintName("FK__Financial__Emplo__4D94879B");
            });

            modelBuilder.Entity<PurposeMaster>(entity =>
            {
                entity.HasKey(e => e.PurposeID).HasName("PK__PurposeM__ED352978E14F1057");

                entity.ToTable("PurposeMaster");

                entity.Property(e => e.PurposeID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PurposeID");

                entity.Property(e => e.PurposeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreditApplicationStatusMaster>(entity =>
            {
                entity.HasKey(e => e.StatusID).HasName("PK__CreditAp__C0F28966696C6703");

                entity.ToTable("CreditApplicationStatusMaster");

                entity.Property(e => e.StatusID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("StatusID");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            // ... (Remaining code)

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
