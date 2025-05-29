using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WikiSystem.Repository;
using WikiSystem.Repository.Implementation.Category;
using WikiSystem.Repository.Implementation.Document;
using WikiSystem.Repository.Implementation.DocumentTag;
using WikiSystem.Repository.Implementation.DocumentVersion;
using WikiSystem.Repository.Implementation.Employee;
using WikiSystem.Repository.Implementation.Tag;
using WikiSystem.Repository.Interfaces.Document;
using WikiSystem.Repository.Interfaces.DocumentVersion;
using WikiSystem.Repository.Interfaces.Employee;
using WikiSystem.Services.DTOs.Authentication;
using WikiSystem.Services.Implementation.Authentication;
using WikiSystem.Services.Interfaces.Authentication;

namespace WikiSystem.Program
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

            var services = new ServiceCollection();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            var serviceProvider = services.BuildServiceProvider();

            //TODO: Add other repositories and services to the service collection

            var employeeRepository = new EmployeeRepository();
            var documentRepository = new DocumentRepository();
            var documentVersionRepository = new DocumentVersionRepository();
            var categoryRepository = new CategoryRepository();
            var documentTagRepository = new DocumentTagRepository();
            var tagRepository = new TagRepository();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nSelect a test to run:");
                Console.WriteLine("1. Retrieve all employees");
                Console.WriteLine("2. Retrieve all documents");
                Console.WriteLine("3. Retrieve document by ID");
                Console.WriteLine("4. Retrieve documents by tag");
                Console.WriteLine("5. Create documentTag");
                Console.WriteLine("9. Test login");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine(new string('-', 20));

                try
                {
                    var authService = serviceProvider.GetRequiredService<IAuthenticationService>();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("\nEmployee list:");
                            var employees = await employeeRepository.RetrieveCollectionAsync(new EmployeeFilter { }).ToListAsync();
                            foreach (var employee in employees)
                            {
                                Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.FullName}, Username: {employee.Username}");
                            }
                            break;

                        case "2":
                            Console.WriteLine("\nDocument list:");
                            var documents = documentRepository.RetrieveCollectionAsync(new DocumentFilter { });
                            var documentVersion = documentVersionRepository.RetrieveCollectionAsync(new DocumentVersionFilter { });
                            //TODO Show tags
                            await foreach (var document in documents)
                            {
                                Console.WriteLine($"Document ID: {document.DocumentId}, Name: {document.Title}");

                                await foreach (var version in documentVersion)
                                {

                                    if (document.DocumentId == version.DocumentId)
                                    {
                                        Console.WriteLine($"Content: {version.Content}, Version: {version.Version}, Create Date: {version.CreateDate}");
                                    }

                                }
                                Console.WriteLine();
                            }
                            break;

                        case "3":
                            Console.Write("Enter document ID (1-20): ");
                            var documentId = int.Parse(Console.ReadLine());
                            var documentById = await documentRepository.RetrieveAsync(documentId);
                            var versionById = await documentVersionRepository.RetrieveAsync(documentId);
                            Console.WriteLine($"Document Name: {documentById.Title}");
                            Console.WriteLine($"Content: {versionById.Content}, Version: {versionById.Version}, Create Date: {versionById.CreateDate}");
                            break;

                        case "4":
                            Console.Write("Enter tag: ");
                            var documentTag = Console.ReadLine();
                            var documentsByTag = await documentRepository.RetrieveCollectionAsync(new DocumentFilter { }).ToListAsync();
                            var documentVersionByTag = await documentVersionRepository.RetrieveCollectionAsync(new DocumentVersionFilter { }).ToListAsync();
                            foreach (var document in documentsByTag)
                            {
                                Console.WriteLine($"Document ID: {document.DocumentId}, Name: {document.Title}");

                                foreach (var version in documentVersionByTag)
                                {

                                    if (document.DocumentId == version.DocumentId)
                                    {
                                        Console.WriteLine($"Content: {version.Content}, Version: {version.Version}, Create Date: {version.CreateDate}");
                                    }

                                }
                                Console.WriteLine();
                            }
                            break;

                        case "5":
                            Console.WriteLine("Enter DocumentId: ");
                            var docId = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter TagId: ");
                            var tagId = int.Parse(Console.ReadLine());

                            var docTag = documentTagRepository.LinkAsync(docId, tagId);

                            Console.WriteLine($"Operation return {docTag.Result}");

                            break;

                        case "9":
                            Console.Write("Enter username: ");
                            var username = Console.ReadLine();
                            Console.Write("Enter password: ");
                            var password = Console.ReadLine();

                            var loginResult = await authService.LoginAsync(new LoginRequest
                            {
                                Username = username,
                                PasswordHash = password
                            });

                            if (!loginResult.Success)
                            {
                                Console.WriteLine($"Login failed: {loginResult.Message}");
                                return;
                            }

                            Console.WriteLine($"Logged in as: {loginResult.FullName}, {loginResult.Role}");
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

                Console.WriteLine();
            }
        }
    }
}