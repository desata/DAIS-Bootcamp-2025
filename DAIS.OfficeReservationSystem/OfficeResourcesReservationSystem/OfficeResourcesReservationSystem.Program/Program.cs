using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OfficeResourcesReservationSystem.Models;
using OfficeResourcesReservationSystem.Repository;
using OfficeResourcesReservationSystem.Repository.Implementations.Employee;
using OfficeResourcesReservationSystem.Repository.Implementations.Reservation;
using OfficeResourcesReservationSystem.Repository.Implementations.Resource;
using OfficeResourcesReservationSystem.Repository.Implementations.ResourceReservation;
using OfficeResourcesReservationSystem.Repository.Implementations.ResourceType;
using OfficeResourcesReservationSystem.Repository.Interfaces.Employee;
using OfficeResourcesReservationSystem.Repository.Interfaces.Reservation;
using OfficeResourcesReservationSystem.Repository.Interfaces.Resource;
using System.Security.Cryptography;
using System.Text;

namespace OfficeResourcesReservationSystem.Program
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            ConnectionFactory.Initialize(connectionString);

            var employeeRepository = new EmployeeRepository();
            var resourceRepository = new ResourceRepository();
            var reservationRepository = new ReservationRepository();
            var resourceReservationRepository = new ResourceReservationRepository();
            var resourceTypeRepository = new ResourceTypeRepository();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nSelect a test to run:");
                Console.WriteLine("1. Retrieve all employees");
                Console.WriteLine("2. Retrieve all resources");
                Console.WriteLine("3. Retrieve resource by ID");
                Console.WriteLine("4. Create new resource");
                Console.WriteLine("5. Delete resource by ID");
                Console.WriteLine("6. Test login");
                // TODO: Console.WriteLine("7. Retrieve all reservations");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine(new string('_', 20));

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("\nEmployee list:");
                            var employees = employeeRepository.RetrieveCollectionAsync(new EmployeeFilter { });
                            await foreach (var employee in employees)
                            {
                                Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.FullName}, Username: {employee.Username}");
                            }
                            break;

                        case "2":
                            Console.WriteLine("\nResource list:");
                            var resources = resourceRepository.RetrieveCollectionAsync(new ResourceFilter { });
                            await foreach (var resource in resources)
                            {
                                Console.WriteLine($"ID: {resource.ResourceId}, Name: {resource.Name}, Type: {resource.ResourceTypeId}");
                            }
                            break;

                        case "3":
                            Console.Write("Enter resource ID (1-20): ");
                            var resourceId = int.Parse(Console.ReadLine());
                            var res = await resourceRepository.RetrieveAsync(resourceId);
                            Console.WriteLine($"Resource Name: {res.Name}");
                            break;

                        case "4":
                            Console.Write("Enter resource name: ");
                            var resourceName = Console.ReadLine();
                            Console.Write("Enter Description: ");
                            var resourceDescription = Console.ReadLine();
                            Console.Write("Is resource available for use? (1 => yes, 0 => no ): ");
                            var isAvailable = int.Parse(Console.ReadLine());
                            Console.WriteLine("1 => Meeting Room\r\n2 => Laptop\r\n3 => Projector\r\n4 => Conference Phone\r\n5 => Video Camera\r\n6 => Printer\r\n7 => Scanner\r\n8 => Whiteboard");
                            Console.Write("Enter resource type ID: ");
                            var resourceTypeId = int.Parse(Console.ReadLine());

                            var newResource = new Models.Resource
                            {
                                Name = resourceName,
                                Description = resourceDescription,
                                IsAvailable = isAvailable == 1,
                                ResourceTypeId = resourceTypeId
                            };

                            var createdId = await resourceRepository.CreateAsync(newResource);
                            Console.WriteLine($"Created resource ID: {createdId}");
                            break;

                        case "5":
                            Console.Write("Enter resource ID to delete: ");
                            var delId = int.Parse(Console.ReadLine());
                            var deleted = await resourceRepository.DeleteAsync(delId);
                            Console.WriteLine($"Deletion successful: {deleted}");
                            break;

                        case "6":
                            Console.Write("Enter username: ");
                            var username = Console.ReadLine();
                            Console.Write("Enter password: ");
                            var password = Console.ReadLine();

                            var hash = HashPassword(password);

                            var userCollection = employeeRepository
                                .RetrieveCollectionAsync(new EmployeeFilter { Username = username })
                                .ToBlockingEnumerable();

                            if (userCollection.Count() == 1 && userCollection.First().PasswordHash == hash)
                            {
                                Console.WriteLine($"Login successful! Welcome {userCollection.First().FullName}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid username or password.");
                            }
                            break;

                        case "0":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select a number from the menu.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred:");
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner exception:");
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }

                Console.WriteLine(new string('_', 40));
            }
        }
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToUpper();
            }
        }
    }
}