using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesApp.API.DomainModels;

namespace NotesApp.API.Data
{
    public class NotesAppDbContext : DbContext
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public NotesAppDbContext(DbContextOptions<NotesAppDbContext> dbContextOptions) : base(dbContextOptions)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.Metadata.FindProperty("ModifiedAt") != null)
                {
                    entityEntry.Property("ModifiedAt").CurrentValue = DateTime.UtcNow;
                }
                if (entityEntry.Metadata.FindProperty("CreatedAt") != null)
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        //entityEntry.Property("UUID").CurrentValue = Guid.NewGuid();
                        entityEntry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
