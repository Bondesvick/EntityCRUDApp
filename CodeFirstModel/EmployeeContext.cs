using ClassModels;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstModel.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (LocalDB)\\MSSQLLocalDB; Initial Catalog = EmployeeAppData");
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.Department).WithMany(d => d.Employees)
            //    .HasForeignKey(s => s.DepartmentId).OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Employee>().Property(e => e.DepartmentId);
        }
    }
}