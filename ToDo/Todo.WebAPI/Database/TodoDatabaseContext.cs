namespace Todo.WebAPI.Database
{
    using Microsoft.EntityFrameworkCore;
    using Todo.WebAPI.Models;

    public class TodoDatabaseContext : DbContext
    {
        public TodoDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskList>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<TaskList>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TaskList>()
                .Property(t => t.Title)
                .IsRequired();
            modelBuilder.Entity<TaskList>()
                .Property(t => t.CreationDate)
                .IsRequired();
            modelBuilder.Entity<TaskList>()
                .HasMany(t => t.Tasks)
                .WithOne(t => t.TaskList)
                .HasForeignKey(t => t.TaskListId)
                .IsRequired();


            modelBuilder.Entity<TaskItem>()
               .HasKey(t => t.Id);
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Title)
                .IsRequired();
            modelBuilder.Entity<TaskItem>()
               .Property(t => t.Status)
               .IsRequired();
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.TaskList);                

            base.OnModelCreating(modelBuilder);
        }
    }
}
