using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ToDoAPI.ToDoDbContext
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = tcp:fofotodoappserver.database.windows.net, 1433; Initial Catalog = ToDoAppDB; Persist Security Info = False; User ID = ToDoAppAdmin; Password = {password}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { ToDoId = 1, Title = "1st Title", Note = "1st Note", IsComplete = false, DeadLine = new DateTime(2024, 06, 25) },
                new ToDoItem { ToDoId = 2, Title = "2nd Title", Note = "2nd Note", IsComplete = false, DeadLine = new DateTime(2024, 07, 25) },
                new ToDoItem { ToDoId = 3, Title = "3rd Title", Note = "3rd Note", IsComplete = false, DeadLine = new DateTime(2024, 08, 25) }
                );
        }

    }
}
