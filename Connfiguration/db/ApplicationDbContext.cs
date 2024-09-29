

using HiManager.Model;
using Microsoft.EntityFrameworkCore;

namespace HiManager.Connfiguration.db;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
    
    public DbSet<Role>Role { get; set; }
    public DbSet<User>User  { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL($"server={Environment.GetEnvironmentVariable("server")};" +
                                $"database={Environment.GetEnvironmentVariable("database")};" +
                                $"user={Environment.GetEnvironmentVariable("user")};" +
                                $"password={Environment.GetEnvironmentVariable("password")}");
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // USER
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithOne(r => r.User)
            .OnDelete(DeleteBehavior.Cascade);

        
        // ROLE 
        modelBuilder.Entity<Role>().HasKey(r => new{r.Uid,r.RoleName});
        modelBuilder.Entity<Role>()
            .HasOne(r => r.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(r => r.Uid);


    }
}