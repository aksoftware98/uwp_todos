using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ToDos.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>()
                .HasKey("Id");

            modelBuilder.Entity<ToDoItem>()
                .ToContainer("Items")
                .HasPartitionKey("Category")
                .OwnsMany<Attachment>("Attachments");
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
