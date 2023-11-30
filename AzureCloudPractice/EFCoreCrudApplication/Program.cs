using EFCoreCrudApplication.CrudManager;

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Add Employee");
    Console.WriteLine("2. Update Employee");
    Console.WriteLine("3. Delete Employee");
    Console.WriteLine("4. Get All Employees");
    Console.WriteLine("5. Add Address for Employee");
    Console.WriteLine("6. Get Combined table data");
    Console.WriteLine("7. Exit");
    Console.Write("Enter your choice: ");

    string choiceInput = Console.ReadLine();

    if (int.TryParse(choiceInput, out int choice))
    {
        switch (choice)
        {
            case 1:
                CrudOperation.AddEmployee();
                break;
            case 2:
                CrudOperation.UpdateEmployee();
                break;
            case 3:
                CrudOperation.DeleteEmployee();
                break;
            case 4:
                CrudOperation.GetAllEmployee();
                break;
            case 5:
                Console.Write("Enter Employee ID: ");
                string empIdInput = Console.ReadLine();
                if (int.TryParse(empIdInput, out int employeeId))
                {
                    Console.Write("Enter Address Details: ");
                    string addressDetails = Console.ReadLine();
                    CrudOperation.AddAddressForEmployee(employeeId, addressDetails);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric Employee ID.");
                }
                break;
            case 6:
                CrudOperation.GetAllCombinedData();
                break;
            case 7:
                isRunning = false;
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter a valid option.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a numeric choice.");
    }
}