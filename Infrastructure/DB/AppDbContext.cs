using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DB;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(task =>
        {
            task.HasOne(t => t.AssignedToUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            task.HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            task.HasOne(t => t.Team)
                .WithMany()
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<User>(user =>
        {
            user.HasIndex(u => u.Email).IsUnique();
            user.HasData(
                new User { Id = 1, FullName = "Admin", Email = "admin@demo.com", Role = Role.Admin },
                new User { Id = 2, FullName = "Manager", Email = "manager@demo.com", Role = Role.Manager },
                new User { Id = 3, FullName = "Employee", Email = "employee@demo.com", Role = Role.Employee }
            );
        });
    }
}
