using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;
using Proje.Areas.Identity.Data;

namespace Proje.Areas.Identity.Data;

public class DBContextSample : IdentityDbContext<SampleUser>
{
    public DBContextSample(DbContextOptions<DBContextSample> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<SampleUser>
{
    public void Configure(EntityTypeBuilder<SampleUser> builder)
    {
        PropertyBuilder propertyBuilder = builder.Property(x => x.kisi_id).UseMySQLAutoIncrementColumn("kisi_id");
        builder.Property(x => x.kisi_isim).HasMaxLength(100);
        builder.Property(x => x.kisi_soyisim).HasMaxLength(100);
        builder.Property(x => x.kisi_para);
        builder.Property(x => x.kisi_type).HasMaxLength(100);

    }
}