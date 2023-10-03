using ToDoList_PG.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoList_PG.Persistence
{
    public class ToDoListDbContext: DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(t =>
            {
            t.HasKey(a=>a.Id);

            t.Property(a=> a.Task_Title).IsRequired();

            t.Property(a=> a.Task_Description)
            .HasColumnType("varchar(200)");

            t.Property(a => a.Start_Date)
            .HasColumnName("StartDate");

            t.Property(a => a.End_Date)
            .HasColumnName("EndDate");

            


            });

        }
    }
}
