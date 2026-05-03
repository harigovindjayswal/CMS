using System;
using System.Collections.Generic;
using CMSDb.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Identity;

public partial class CmsIdentityContext : IdentityDbContext<User>
{
    public CmsIdentityContext()
    {
    }

    public CmsIdentityContext(DbContextOptions<CmsIdentityContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .Property(u => u.CreatedDate)
        .HasDefaultValueSql("GETDATE()");
        base.OnModelCreating(modelBuilder);
    }
}
