using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using WikiSystem.Repository;
using WikiSystem.Repository.Implementations.Category;
using WikiSystem.Repository.Implementations.Document;
using WikiSystem.Repository.Implementations.DocumentVersion;
using WikiSystem.Repository.Implementations.Employee;
using WikiSystem.Repository.Interfaces.Document;
using WikiSystem.Repository.Interfaces.DocumentVersion;
using WikiSystem.Repository.Interfaces.Employee;

namespace WikiSystem.App
{
    public class Program
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
            var documentRepository = new DocumentRepository();
            var documentVersionRepository = new DocumentVersionRepository();
            var categoryRepository = new CategoryRepository();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nSelect a test to run:");
                Console.WriteLine("1. Retrieve all employees");
                Console.WriteLine("2. Retrieve all documents");
                Console.WriteLine("3. Retrieve document by ID");
                Console.WriteLine("4. Test login");
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
                            Console.WriteLine("\nDocument list:");
                            var documents = documentRepository.RetrieveCollectionAsync(new DocumentFilter { });
                            var documentVersion = documentVersionRepository.RetrieveCollectionAsync(new DocumentVersionFilter { });
                            await foreach (var document in documents)
                            {
                                Console.WriteLine($"Document ID: {document.DocumentId}, Name: {document.Title}, Type: {document.Tags}");

                                await foreach (var version in documentVersion)
                                {

                                    if (document.DocumentId == version.DocumentId)
                                    {
                                        Console.WriteLine($"Content: {version.Content}, Version: {version.Version}, Create Date: {version.CreateDate}");
                                    }

                                }
                                Console.WriteLine(new string('_', 20));
                            }
                            break;

                        case "3":
                            Console.Write("Enter resource ID (1-20): ");
                            var documentId = int.Parse(Console.ReadLine());
                            var documentById = await documentRepository.RetrieveAsync(documentId);
                            var versionById = await documentVersionRepository.RetrieveAsync(documentId);
                            Console.WriteLine($"Document Name: {documentById.Title}, Tags: {documentById.Tags}");
                            Console.WriteLine($"Content: {versionById.Content}, Version: {versionById.Version}, Create Date: {versionById.CreateDate}");
                            break;

                        case "4":
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