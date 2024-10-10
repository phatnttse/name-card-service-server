using Demo_Grapesjs.Entities;
using Microsoft.EntityFrameworkCore;


namespace Demo_Grapesjs.Core.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NameCardTemplate> NameCardTemplates { get; set; }
        public DbSet<UserNameCard> UserNameCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ giữa User và UserNameCard
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserNameCards)
                .WithOne(unc => unc.User)
                .HasForeignKey(unc => unc.UserId); // Khóa ngoại trong UserNameCard

            // Cấu hình quan hệ giữa NameCardTemplate và UserNameCard
            modelBuilder.Entity<NameCardTemplate>()
                .HasMany(nct => nct.UserNameCards)
                .WithOne(unc => unc.NameCardTemplate)
                .HasForeignKey(unc => unc.NameCardTemplateId); // Khóa ngoại trong UserNameCard
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddAuditInfo()
        {

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity &&
                           (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (BaseEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                }

                entity.UpdatedAt = now;
            }
        }
    }
}
