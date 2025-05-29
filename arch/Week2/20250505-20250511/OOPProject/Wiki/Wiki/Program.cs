using System.ComponentModel.Design;
using System.Linq;
using Wiki.Models;
using Wiki.Services;

namespace Wiki
{
    public class Program
    {
        static void Main(string[] args)
        {
            var repo = new WikiRepository();
            var categories = new List<Category>();
            var documents = new List<Document>();
            int documentid = 5;//TODO should be static
            int categoryid = 1;//TODO should be static

            var admin = new User { UserId = "u1", Name = "Ted", Role = Role.Admin, AccessLevel = AccessLevel.Internal };
            var editor = new User { UserId = "u2", Name = "Bob", Role = Role.Editor, AccessLevel = AccessLevel.Internal };
            var editor1 = new User { UserId = "u3", Name = "Fill", Role = Role.Editor, AccessLevel = AccessLevel.Internal };
            var editor2 = new User { UserId = "u4", Name = "Joe", Role = Role.Editor, AccessLevel = AccessLevel.Internal };
            var editor3 = new User { UserId = "u5", Name = "Rob", Role = Role.Editor, AccessLevel = AccessLevel.Internal };            

            var documentsSeed = new List<(string title, string content, string[] tags, string categoryName)>
        {
            ("C# Basics", "Introduction to C# programming language.", new[] { "CSharp", "Beginner", "Programming" }, "Development"),
            ("Agile Methodologies", "Overview of Agile frameworks including Scrum and Kanban.", new[] { "Agile", "Scrum", "ProjectManagement" }, "Management"),
            ("Database Optimization", "Tips and tricks for optimizing SQL queries.", new[] { "SQL", "Performance", "Database" }, "Data"),
            ("Security Guidelines", "Best practices for securing web applications.", new[] { "Security", "Web", "OWASP" }, "IT"),
            ("Effective Communication", "Improving team communication skills.", new[] { "SoftSkills", "Teamwork", "Communication" }, "HR")
        };

            Console.WriteLine("\nWelcome to Wiki!");
            Console.WriteLine("\nPlease choose an option from below:");
            Console.WriteLine("1. Upload document");
            Console.WriteLine("2. Search by Tag");
            Console.WriteLine("3. Search by Title");
            //Console.WriteLine("4. Add to collection");
            Console.WriteLine("5. Archive old documents");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choise: ");
            Console.WriteLine();

            string input = Console.ReadLine();
            Console.WriteLine();

            while (input != "0")
            {
                if (input == "1")
                {
                    documentid++;
                    string id = (documentid).ToString();
                    Console.Write("Enter title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter content: ");
                    string content = Console.ReadLine();
                    Console.Write("Enter category: ");
                    string category = Console.ReadLine();
                    Console.Write("Enter tags (comma-separated): ");
                    string[] tags = Console.ReadLine().Split(',');
                    Console.Write("Enter version number: ");
                    string version = Console.ReadLine();

                    if (repo.FindSpecificDocuments(title, content, int.Parse(version)).Any())
                    {
                        Console.WriteLine("Document already exists with the same version, title and content.");
                    }
                    else
                    {
                        Category docCategory;

                        if (!categories.Any(c => c.Name == category))
                        {
                            docCategory = new Category
                            {
                                Id = categoryid,
                                Name = category
                            };
                        }
                        else
                        {
                            docCategory = categories.FirstOrDefault(c => c.Name == category);
                        }


                        var document = new Document
                        {
                            Id = id,
                            Title = title,
                            Content = content,
                            Tags = new List<string>(tags),
                            Category = docCategory,
                            AccessLevel = AccessLevel.Internal
                        };

                        repo.AddDocument(editor, document);
                        repo.AddVersion(version);
                        categoryid++;
                    }
                }
                else if (input == "2")
                {
                    Console.Write("Enter tag to search: ");
                    string tag = Console.ReadLine();

                    var searchResults = repo.SearchDocumentsbyTag(tag);
                    if (searchResults.Count == 0)
                    {
                        Console.WriteLine("No documents found with that tag.");
                    }
                    else
                    {
                        Console.WriteLine($"Find ({searchResults.Count}) Results:");
                        foreach (var result in searchResults)
                            Console.WriteLine($" - {result.Title}");

                    }
                }
                else if (input == "3")
                {
                    Console.Write("Enter title to search: ");
                    string title = Console.ReadLine();

                    var searchResults = repo.SearchDocumentsbyTitle(title);
                    if (searchResults.Count == 0)
                    {
                        Console.WriteLine("No documents found with that title.");

                    }
                    else
                    {
                        Console.WriteLine($"Find ({searchResults.Count}) Results:");
                        foreach (var result in searchResults)
                            Console.WriteLine($" - {result.Title}");

                    }
                }

                else if (input == "5")
                {
                    Console.Write("Enter document ID to archive: ");
                    string documentId = Console.ReadLine();
                    var document = repo.FindSpecificDocumentByID(documentId);
                    if (document != null)
                    {
                        repo.ArchiveOldDocuments(document);
                        Console.WriteLine($"Document '{document.Title}' archived.");
                    }
                    else
                    {
                        Console.WriteLine("Document not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
                Console.WriteLine("\nPlease choose an option from below:");
                Console.WriteLine("1. Upload document");
                Console.WriteLine("2. Search by Tag");
                Console.WriteLine("3. Search by Title");
                //Console.WriteLine("4. Search by Content");
                Console.WriteLine("5. Archive old documents");
                Console.WriteLine("0. Exit");
                input = Console.ReadLine();
            }
            if (input == "0")
            {
                Console.WriteLine("Exiting...");
                return;
            }
        }
    }
}