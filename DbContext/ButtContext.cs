using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ButtButton.Database;

public class Message
{
    [Key] // Ensures it's the primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies auto-increment
    public int Id { get; set; } // Primary Key

    public string Author { get; set; }
    public string Content { get; set; } // Renamed to Content to avoid confusion
}

public class Click
{
    [Key] // Ensures it's the primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies auto-increment
    public int Id { get; set; } // Primary Key

    public DateTime Timestamp { get; set; }
}

// DbContext definition
public class ButtContext : DbContext
{
    public ButtContext(DbContextOptions<ButtContext> options)
        : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }
    public DbSet<Click> Clicks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customize model if needed (optional)
        modelBuilder.Entity<Message>(message =>
        {
            message.HasKey(e => e.Id); // Set as primary key
            message.Property(e => e.Id)
                .ValueGeneratedOnAdd(); // Auto-increment
        });
        
        modelBuilder.Entity<Click>(message =>
        {
            message.HasKey(e => e.Id); // Set as primary key
            message.Property(e => e.Id)
                .ValueGeneratedOnAdd(); // Auto-increment
        });
    }

    // Auto-migrate on startup
    public void MigrateDatabase()
    {
        Database.Migrate();
    }
}