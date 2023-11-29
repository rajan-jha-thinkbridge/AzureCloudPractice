using EFCoreCrudApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCrudApplication.Dbcontext
{
    public class EmployeeDbContext :DbContext
    {
        public DbSet<Employee> employees { get; set; }

        public DbSet<Address> Addresse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-HCF92PN\SQLEXPRESS01;Initial Catalog=EmployeeData;Integrated Security=True;Encrypt=False;");
        }
    }
}
