using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationTemplate.Models;

public class ApplicationContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public DbSet<ProjectState> Project { get; set; } = default!;


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var userId = GetAuthenticatedUserId();

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId!;
                    entry.Entity.ModifiedBy = userId!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.ModifiedDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = userId!;
                    entry.Entity.ModifiedDate = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    private string? GetAuthenticatedUserId()
    {
        // This will get the UserId of the current logged in user.
        var user = _httpContextAccessor.HttpContext?.User;
        return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
