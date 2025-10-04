using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CMSDb.DbModels;

public partial class CmsContext : DbContext
{
    public CmsContext()
    {
    }

    public CmsContext(DbContextOptions<CmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentDtl> DocumentDtls { get; set; }

    public virtual DbSet<DocumentMaster> DocumentMasters { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<OptionMst> OptionMsts { get; set; }

    public virtual DbSet<Otpdtl> Otpdtls { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5499A813DDEF67");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(200);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.TableName).HasMaxLength(100);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditLogs_Users");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.CaseId).HasName("PK__Cases__6CAE526C7FBA9E16");

            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.AssignedTo).HasMaxLength(450);
            entity.Property(e => e.CaseType).HasMaxLength(100);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.CourtName).HasMaxLength(150);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Opponent).HasMaxLength(150);
            entity.Property(e => e.Stage)
                .HasMaxLength(20)
                .HasDefaultValue("Filed");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Open");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Cases)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK_Cases_Users");

            entity.HasOne(d => d.Client).WithMany(p => p.Cases)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cases_Clients");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A042F9D710B");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Clients_Users");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6F6F67F17C");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(150);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UploadedBy).HasMaxLength(450);
            entity.Property(e => e.Version).HasDefaultValue(1);

            entity.HasOne(d => d.Case).WithMany(p => p.Documents)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documents_Cases");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documents_Users");
        });

        modelBuilder.Entity<DocumentDtl>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApplicationNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DigiPath)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Digi_Path");
            entity.Property(e => e.DocId).HasColumnName("Doc_ID");
            entity.Property(e => e.DocPath)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Doc_Path");
            entity.Property(e => e.Flag)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("flag");
            entity.Property(e => e.IsDigiSign).HasColumnName("Is_DigiSign");
            entity.Property(e => e.Uploaddatetime)
                .HasColumnType("datetime")
                .HasColumnName("uploaddatetime");
        });

        modelBuilder.Entity<DocumentMaster>(entity =>
        {
            entity.ToTable("Document_Master");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DigiDocName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DigiDoc_Name");
            entity.Property(e => e.DigiMandatory)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Digi_Mandatory");
            entity.Property(e => e.DocDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Doc_Desc");
            entity.Property(e => e.DocId).HasColumnName("Doc_ID");
            entity.Property(e => e.DocName)
                .HasMaxLength(1000)
                .HasColumnName("Doc_Name");
            entity.Property(e => e.FileDirectory)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("File_Directory");
            entity.Property(e => e.FileExtension)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("File_Extension");
            entity.Property(e => e.FileSizeBytes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("File_SizeBytes");
            entity.Property(e => e.Flag)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Mandatory)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C87055661586");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventType).HasMaxLength(20);
            entity.Property(e => e.Reminder).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.Case).WithMany(p => p.Events)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Cases");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_Users");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD5ED8AE495");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Case).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Cases");

            entity.HasOne(d => d.Client).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Clients");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__InvoiceI__727E83EBC97016ED");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.Total)
                .HasComputedColumnSql("([Quantity]*[UnitPrice])", true)
                .HasColumnType("decimal(21, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceItems_Invoices");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C037C47261C2F");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.FromUserId)
                .HasMaxLength(450)
                .HasColumnName("FromUserID");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ToUserId)
                .HasMaxLength(450)
                .HasColumnName("ToUserID");

            entity.HasOne(d => d.Case).WithMany(p => p.Messages)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Cases");

            entity.HasOne(d => d.FromUser).WithMany(p => p.MessageFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_FromUser");

            entity.HasOne(d => d.ToUser).WithMany(p => p.MessageToUsers)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_ToUser");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Notes__EACE357FD65BA5B4");

            entity.Property(e => e.NoteId).HasColumnName("NoteID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsPrivate).HasDefaultValue(true);
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Case).WithMany(p => p.Notes)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notes_Cases");

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notes_Users");
        });

        modelBuilder.Entity<OptionMst>(entity =>
        {
            entity.HasKey(e => e.SerialNo);

            entity.ToTable("OptionMst");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OptionCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.OptionDesc).HasMaxLength(100);
            entity.Property(e => e.OptionDescHindi).HasMaxLength(200);
            entity.Property(e => e.OptionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Otpdtl>(entity =>
        {
            entity.HasKey(e => e.Sno).HasName("PK_CommanOTPtbl");

            entity.ToTable("OTPDtls");

            entity.Property(e => e.Sno).HasColumnName("SNO");
            entity.Property(e => e.ApplicationNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Flag)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InstDate).HasColumnType("datetime");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Otp)
                .HasMaxLength(250)
                .HasColumnName("OTP");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D14186CB16");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.AssignedTo).HasMaxLength(450);
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("To-Do");
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Users");

            entity.HasOne(d => d.Case).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Cases");
        });
        modelBuilder.HasSequence("RS001_SEQ");
        modelBuilder.HasSequence("RS002_SEQ");
        modelBuilder.HasSequence("RSKR_SEQ");
        modelBuilder.HasSequence("RSRM_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
