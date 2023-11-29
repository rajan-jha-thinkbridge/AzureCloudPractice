using System.ComponentModel.DataAnnotations;

namespace EFCoreCrudApplication.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; } // One-to-many relationship with Address

    }
}
