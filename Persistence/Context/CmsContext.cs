using System;
using System.Collections.Generic;
using System.Reflection;
using Domain.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class CmsContext : DbContext
{
    public CmsContext(DbContextOptions<CmsContext> options)
        : base(options)
    {
    }

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

    public virtual DbSet<CaseTask> Tasks { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<decimal>()
               .HavePrecision(18, 2);
    }

}
