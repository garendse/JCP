using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JCPBackend.Models;

public partial class JobCostProTestingContext : DbContext
{
    public JobCostProTestingContext(DbContextOptions<JobCostProTestingContext> options)
        : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public virtual DbSet<j_customer> j_customers { get; set; }

    public virtual DbSet<j_job_code> j_job_codes { get; set; }

    public virtual DbSet<j_quote> j_quotes { get; set; }

    public virtual DbSet<j_quote_item> j_quote_items { get; set; }

    public virtual DbSet<j_quote_item_supplier> j_quote_item_suppliers { get; set; }

    public virtual DbSet<j_quote_status> j_quote_statuses { get; set; }

    public virtual DbSet<j_part_order> j_sent_emails { get; set; }

    public virtual DbSet<j_site> j_sites { get; set; }

    public virtual DbSet<j_supplier> j_suppliers { get; set; }

    public virtual DbSet<j_supplier_branch> j_supplier_branches { get; set; }

    public virtual DbSet<j_tech> j_techs { get; set; }

    public virtual DbSet<j_user> j_users { get; set; }

    public virtual DbSet<j_vehicle> j_vehicles { get; set; }

    public virtual DbSet<j_vehicle_model> j_vehicle_models { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<j_customer>(entity =>
        {
            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.address_line_1).HasMaxLength(50);
            entity.Property(e => e.address_line_2).HasMaxLength(50);
            entity.Property(e => e.address_line_3).HasMaxLength(50);
            entity.Property(e => e.alt_no).HasMaxLength(50);
            entity.Property(e => e.company_name).HasMaxLength(100);
            entity.Property(e => e.email).HasMaxLength(50);
            entity.Property(e => e.home_no)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.mobile_no)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.postal).HasMaxLength(50);
            entity.Property(e => e.reg_number).HasMaxLength(50);
            entity.Property(e => e.site_access_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.surname).HasMaxLength(50);
            entity.Property(e => e.title).HasMaxLength(20);
            entity.Property(e => e.type).HasMaxLength(10);
            entity.Property(e => e.vat_no).HasMaxLength(50);
            entity.Property(e => e.work_no).HasMaxLength(50);
        });

        modelBuilder.Entity<j_job_code>(entity =>
        {
            entity.HasKey(e => new { e.code, e.site_access_id });

            entity.Property(e => e.code).HasMaxLength(50);
            entity.Property(e => e.site_access_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.description).HasMaxLength(255);
            entity.Property(e => e.location).HasMaxLength(50);
        });

        modelBuilder.Entity<j_quote>(entity =>
        {
            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.branch_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.create_user_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.customer_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ro_number).HasMaxLength(20);
            entity.Property(e => e.site_access)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.status).HasMaxLength(50);
            entity.Property(e => e.tech_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.update_user_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.vehicle_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.create_user).WithMany(p => p.j_quotecreate_users)
                .HasForeignKey(d => d.create_user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_create_user");

            entity.HasOne(d => d.customer).WithMany(p => p.j_quotes)
                .HasForeignKey(d => d.customer_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_customer");

            entity.HasOne(d => d.tech).WithMany(p => p.j_quotes)
                .HasForeignKey(d => d.tech_id)
                .HasConstraintName("FK_tech");

            entity.HasOne(d => d.update_user).WithMany(p => p.j_quoteupdate_users)
                .HasForeignKey(d => d.update_user_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_update_user");

            entity.HasOne(d => d.vehicle).WithMany(p => p.j_quotes)
                .HasForeignKey(d => d.vehicle_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_vehicle");
        });

        modelBuilder.Entity<j_quote_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_j_quote_items_1");

            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.description).HasMaxLength(255);
            entity.Property(e => e.job_code).HasMaxLength(10);
            entity.Property(e => e.location).HasMaxLength(50);
            entity.Property(e => e.quote_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.quote).WithMany(p => p.items)
                .HasForeignKey(d => d.quote_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_items");
        });

        modelBuilder.Entity<j_quote_item_supplier>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_j_quote_item_quotes");

            entity.ToTable("j_quote_item_supplier");

            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.accepted_by_user_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.part_number).HasMaxLength(100);
            entity.Property(e => e.quote_item_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.quoted_by).HasMaxLength(100);
            entity.Property(e => e.remarks).HasColumnType("text");
            entity.Property(e => e.supplier_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.user).WithMany(p => p.j_quote_item_suppliers)
                .HasForeignKey(d => d.accepted_by_user_id)
                .HasConstraintName("FK_j_quote_item_supplier_j_users");

            entity.HasOne(d => d.quote_item).WithMany(p => p.subquotes)
                .HasForeignKey(d => d.quote_item_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_j_quote_item_supplier_j_quote_items");

            entity.HasOne(d => d.supplier).WithMany(p => p.j_quote_item_suppliers)
                .HasForeignKey(d => d.supplier_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_supplier");
        });

        modelBuilder.Entity<j_quote_status>(entity =>
        {
            entity.HasKey(e => e.status);

            entity.ToTable("j_quote_status");

            entity.Property(e => e.status).HasMaxLength(50);
        });

        modelBuilder.Entity<j_site>(entity =>
        {
            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.description).HasMaxLength(50);
        });

        modelBuilder.Entity<j_supplier>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_j_suppliers_1");

            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.address_line_1).HasMaxLength(50);
            entity.Property(e => e.address_line_2).HasMaxLength(50);
            entity.Property(e => e.address_line_3).HasMaxLength(50);
            entity.Property(e => e.after_hours_no).HasMaxLength(12);
            entity.Property(e => e.contact_no).HasMaxLength(12);
            entity.Property(e => e.contact_person).HasMaxLength(50);
            entity.Property(e => e.email).HasMaxLength(320);
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.postal).HasMaxLength(10);
            entity.Property(e => e.reg_no).HasMaxLength(50);
            entity.Property(e => e.standby_email).HasMaxLength(320);
            entity.Property(e => e.standby_no).HasMaxLength(12);
            entity.Property(e => e.standby_person).HasMaxLength(50);
            entity.Property(e => e.tax_clearance).HasMaxLength(50);
            entity.Property(e => e.tel_num).HasMaxLength(12);
            entity.Property(e => e.vat_no).HasMaxLength(50);
        });

        modelBuilder.Entity<j_supplier_branch>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_j_suppliers");

            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.address_line_1).HasMaxLength(50);
            entity.Property(e => e.address_line_2).HasMaxLength(50);
            entity.Property(e => e.address_line_3).HasMaxLength(50);
            entity.Property(e => e.contact_number).HasMaxLength(50);
            entity.Property(e => e.contact_person).HasMaxLength(50);
            entity.Property(e => e.email).HasMaxLength(320);
            entity.Property(e => e.lat).HasColumnType("decimal(16, 8)");
            entity.Property(e => e.lgn).HasColumnType("decimal(16, 8)");
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.postal).HasMaxLength(10);
            entity.Property(e => e.supplier_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.supplier).WithMany(p => p.supplier)
                .HasForeignKey(d => d.supplier_id)
                .HasConstraintName("FK_j_supplier_branches_j_suppliers");
        });

        modelBuilder.Entity<j_tech>(entity =>
        {
            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.name).HasMaxLength(30);
            entity.Property(e => e.site_access_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.surname).HasMaxLength(30);
        });

        modelBuilder.Entity<j_user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK_j_users_1");

            entity.HasIndex(e => e.username, "IX_j_users_1").IsUnique();

            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.end_date).HasColumnType("date");
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.password)
                .HasMaxLength(50);
            entity.Property(e => e.password_date).HasColumnType("date");
            entity.Property(e => e.role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.surname).HasMaxLength(50);
            entity.Property(e => e.tel_no)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.username).HasMaxLength(30);

            entity.HasMany(d => d.sites).WithMany(p => p.users)
                .UsingEntity<Dictionary<string, object>>(
                    "j_user_site_access",
                    r => r.HasOne<j_site>().WithMany()
                        .HasForeignKey("site_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_j_user_site_access_j_sites"),
                    l => l.HasOne<j_user>().WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_j_user_site_access_j_user_site_access"),
                    j =>
                    {
                        j.HasKey("user_id", "site_id");
                        j.ToTable("j_user_site_access");
                    });
        });

        modelBuilder.Entity<j_vehicle>(entity =>
        {
            entity.Property(e => e.id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.brand).HasMaxLength(50);
            entity.Property(e => e.customer_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.engine_number)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.model).HasMaxLength(50);
            entity.Property(e => e.registration).HasMaxLength(15);
            entity.Property(e => e.site_access_id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.vin_number)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<j_vehicle_model>(entity =>
        {
            entity.HasKey(e => new { e.brand, e.model });

            entity.Property(e => e.brand).HasMaxLength(50);
            entity.Property(e => e.model).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
