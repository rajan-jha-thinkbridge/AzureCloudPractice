using System.ComponentModel.DataAnnotations.Schema;


namespace EFCoreCrudApplication.Model
{
    public class Address
    {
        public int AddressId { get; set; }

        public int EmployeeId { get; set; } // Foreign key to link to Employee

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } // Navigation property to access the related Employee

        public string AddressDetails { get; set; } // New field to store the address details
    }

}
