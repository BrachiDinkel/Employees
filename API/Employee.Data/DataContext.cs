using Employee.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Core
{
    public class DataContext : DbContext
    {
        public DbSet<EmployeeDetails> Employee { get; set; }
        public DbSet<EmployeeRole> EmployeeRole { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EmployeesDataBase");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeRole>()
                .HasKey(pe => new { pe.EmployeeId, pe.RoleId });
        }




    }
}