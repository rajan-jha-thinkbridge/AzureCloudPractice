using EFCoreCrudApplication.Dbcontext;
using EFCoreCrudApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCrudApplication.CrudManager
{
    public class CrudOperation
    {
        public static void addEmployee()
        {
            using (var dbContext = new EmployeeDbContext())
            {
                Employee employee = new Employee();
                Console.WriteLine("Enter the name of the employee:");
                employee.Name = Console.ReadLine();

                dbContext.Add(employee);
                dbContext.SaveChanges();

                Console.WriteLine("Employee Added succefully");
            };
        }
        public static void DeleteEmployee()
        {
            using (var dbContext = new EmployeeDbContext())
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int id))
                {
                    var employeeToDelete = dbContext.employees.FirstOrDefault(e => e.Id == id);

                    if (employeeToDelete != null)
                    {
                        dbContext.employees.Remove(employeeToDelete);
                        dbContext.SaveChanges();

                        Console.WriteLine("Employee Deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Employee with the specified ID does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the employee ID.");
                }
            }
        }
        public static void UpdateEmployee()
        {
            using (var dbContext = new EmployeeDbContext())
            {
                Console.WriteLine("Enter the Id of the employee:");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int id))
                {
                    var employeeToUpdate = dbContext.employees.FirstOrDefault(e => e.Id == id);

                    if (employeeToUpdate != null)
                    {
                        Console.WriteLine("Enter the new name for the employee:");
                        string newName = Console.ReadLine();

                        // Update the employee's name
                        employeeToUpdate.Name = newName;

                        dbContext.SaveChanges();

                        Console.WriteLine("Employee Updated successfully");
                    }
                    else
                    {
                        Console.WriteLine("Employee with the specified ID does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the employee ID.");
                }
            }
        }
        public static void GetAllEmployee()
        {
            using (var dbContext = new EmployeeDbContext())
            {
                Console.WriteLine("ID\tName");
                Console.WriteLine("-----------------------------------");

                foreach (var employee in dbContext.employees)
                {
                    Console.WriteLine($"{employee.Id}\t{employee.Name}");
                }
            }
        }

        public static void AddAddressForEmployee(int employeeId, string addressDetails)
        {
            using (var dbContext = new EmployeeDbContext())
            {
                // Find the employee with the specified ID
                var employee = dbContext.employees.FirstOrDefault(e => e.Id == employeeId);

                if (employee != null)
                {
                    // Initialize the Addresses collection if it's null
                    if (employee.Addresses == null)
                    {
                        employee.Addresses = new List<Address>();
                    }

                    // Create a new Address object and set its properties
                    Address address = new Address
                    {
                        EmployeeId = employeeId,
                        AddressDetails = addressDetails
                    };

                    // Add the address to the Addresses collection of the employee
                    employee.Addresses.Add(address);

                    // Save changes to the database
                    dbContext.SaveChanges();

                    Console.WriteLine("Address added successfully for the employee.");
                }
                else
                {
                    Console.WriteLine("Employee with the specified ID does not exist.");
                }
            }
        }

        public static void GetAllCombinedData()
        {
            using (var dbContext = new EmployeeDbContext())
            {
                // Ensure both tables have data before performing the join
                if (dbContext.employees.Any() && dbContext.Addresse.Any())
                {
                    var combinedData = dbContext.employees
                        .Join(dbContext.Addresse,
                            employee => employee.Id,
                            address => address.EmployeeId,
                            (employee, address) => new
                            {
                                employee.Id,
                                employee.Name,
                                address.AddressDetails
                            })
                        .ToList();

                    if (combinedData.Any())
                    {
                        Console.WriteLine("Combined Data (Employee and Address):");
                        Console.WriteLine("ID\tName\tAddress Details");
                        Console.WriteLine("-------------------------------------------");

                        foreach (var data in combinedData)
                        {
                            Console.WriteLine($"{data.Id}\t{data.Name}\t{data.AddressDetails}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No combined data found. Make sure both Employee and Address tables have data.");
                    }
                }
                else
                {
                    Console.WriteLine("Either the Employee table or the Address table is empty. Make sure both tables have data.");
                }
            }
        }
    }
}
