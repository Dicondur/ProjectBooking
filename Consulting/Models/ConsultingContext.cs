using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Consulting.Models
{
    public partial class ConsultingContext : DbContext
    {
        public virtual DbSet<Consultant> Consultant { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<WorkSession> WorkSession { get; set; }

        public ConsultingContext(DbContextOptions<ConsultingContext> options)
             : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultant>(entity =>
            {
                entity.ToTable("consultant");

                entity.Property(e => e.ConsultantId).HasColumnName("consultantId");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.DateHired)
                    .HasColumnName("dateHired")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HourlyRate).HasColumnName("hourlyRate");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Partner).HasColumnName("partner");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contract");

                entity.Property(e => e.ContractId).HasColumnName("contractId");

                entity.Property(e => e.Closed).HasColumnName("closed");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.DaysDuration).HasColumnName("daysDuration");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QuotedPrice).HasColumnName("quotedPrice");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("date");

                entity.Property(e => e.TotalChargedToDate)
                    .HasColumnName("totalChargedToDate")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contract_FK00");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("companyName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactFirstName)
                    .HasColumnName("contactFirstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactLastName)
                    .HasColumnName("contactLastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postalCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceCode)
                    .HasColumnName("provinceCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxExempt).HasColumnName("taxExempt");

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.ProvinceCode)
                    .HasConstraintName("customer_FK00");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvinceCode)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("province");

                entity.Property(e => e.ProvinceCode)
                    .HasColumnName("provinceCode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvincialTax).HasColumnName("provincialTax");
            });

            modelBuilder.Entity<WorkSession>(entity =>
            {
                entity.ToTable("workSession");

                entity.Property(e => e.WorkSessionId).HasColumnName("workSessionId");

                entity.Property(e => e.ConsultantId).HasColumnName("consultantId");

                entity.Property(e => e.ContractId).HasColumnName("contractId");

                entity.Property(e => e.DateWorked)
                    .HasColumnName("dateWorked")
                    .HasColumnType("date");

                entity.Property(e => e.HourlyRate).HasColumnName("hourlyRate");

                entity.Property(e => e.HoursWorked).HasColumnName("hoursWorked");

                entity.Property(e => e.ProvincialTax).HasColumnName("provincialTax");

                entity.Property(e => e.TotalChargeBeforeTax).HasColumnName("totalChargeBeforeTax");

                entity.Property(e => e.WorkDescription)
                    .HasColumnName("workDescription")
                    .IsUnicode(false);

                entity.HasOne(d => d.Consultant)
                    .WithMany(p => p.WorkSession)
                    .HasForeignKey(d => d.ConsultantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("workSession_FK00");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.WorkSession)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("workSession_FK01");
            });
        }
    }
}
