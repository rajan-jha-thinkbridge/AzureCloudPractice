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
        public static void AddEmployee()
        {
            try
            {
                using (var dbContext = new EmployeeDbContext())
                {
                    Employee employee = new Employee();
                    Console.WriteLine("Enter the name of the employee:");
                    employee.Name = Console.ReadLine();

                    dbContext.Add(employee);
                    dbContext.SaveChanges();

                    Console.WriteLine("Employee Added successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void DeleteEmployee()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void UpdateEmployee()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public static void GetAllEmployee()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void AddAddressForEmployee(int employeeId, string addressDetails)
        {
            try
            {
                using (var dbContext = new EmployeeDbContext())
                {
                    var employee = dbContext.employees.FirstOrDefault(e => e.Id == employeeId);

                    if (employee != null)
                    {
                        if (employee.Addresses == null)
                        {
                            employee.Addresses = new List<Address>();
                        }

                        Address address = new Address
                        {
                            EmployeeId = employeeId,
                            AddressDetails = addressDetails
                        };

                        employee.Addresses.Add(address);

                        dbContext.SaveChanges();

                        Console.WriteLine("Address added successfully for the employee.");
                    }
                    else
                    {
                        Console.WriteLine("Employee with the specified ID does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void GetAllCombinedData()
        {
            try
            {
                using (var dbContext = new EmployeeDbContext())
                {
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}
