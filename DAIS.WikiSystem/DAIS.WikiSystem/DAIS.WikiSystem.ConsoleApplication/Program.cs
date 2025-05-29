using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository;
using DAIS.WikiSystem.Repository.Implementation.Collection;
using DAIS.WikiSystem.Repository.Implementation.Document;
using DAIS.WikiSystem.Repository.Implementation.DocumentVersion;
using DAIS.WikiSystem.Repository.Implementation.Mappers;
using DAIS.WikiSystem.Repository.Implementation.Tag;
using DAIS.WikiSystem.Repository.Interfaces.Tag;
using DAIS.WikiSystem.Services.DTOs.Collection;
using DAIS.WikiSystem.Services.DTOs.Document;
using DAIS.WikiSystem.Services.DTOs.DocumentVersion;
using DAIS.WikiSystem.Services.DTOs.User;
using DAIS.WikiSystem.Services.Implementation.Collection;
using DAIS.WikiSystem.Services.Implementation.Document;
using DAIS.WikiSystem.Services.Implementation.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.Collection;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace DAIS.WikiSystem.ConsoleApplication
{
    class TestUserService : Services.Interfaces.User.IUserService
    {
        public int UserId => 3;
        public Role Role => Role.Admin;
        public AccessLevel AccessLevel => AccessLevel.Internal;

        public Task<GetUserResponse> GetByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            ConnectionFactory.Initialize(connectionString);


            var userContext = new TestUserService();

            // --- 2) Instantiate repositories (pass your real connection string) ---

            var docRepo = new DocumentRepository();
            var verRepo = new DocumentVersionRepository();
            var collRepo = new CollectionRepository();
            var mapRepo = new CollectionDocumentRepository();
            var tagRepo = new TagRepository();
            var docTagRepo = new DocumentTagRepository();

            // --- 3) Instantiate services ---
            IDocumentService docService = new DocumentService(docRepo, userContext, docTagRepo, tagRepo);

            IDocumentVersionService verService = new DocumentVersionService(verRepo);
            ICollectionService collService = new CollectionService(collRepo, mapRepo);
            //ICollectionDocumentService mapService = new CollectionDocumentService(mapRepo);

            // —————————————————————————————
            // 4) Create a new Document
            // ———————————————— Test: Create Document with Tags ————————————————
            // ——————————————————————————————————————————
            //  Fetch a few existing tag IDs
            Console.WriteLine("Fetching existing tags from DB...");
            var existingTags = await tagRepo
                .RetrieveCollectionAsync(new TagFilter())   // no filter = all tags
                .Take(3)                                     // just pick 3
                .ToListAsync();

            if (existingTags.Count == 0)
            {
                Console.WriteLine("✖ No tags found in the database. Aborting test.");
                return;
            }

            var tagIds = existingTags.Select(t => t.TagId).ToList();
            Console.WriteLine($"  Using Tag IDs: {string.Join(", ", tagIds)}");
            // —————————————————————————————
            Console.WriteLine("\nCreating document with tags...");
            Console.WriteLine("\nEnter document name");
            var newTitle = Console.ReadLine();
            var createReq = new CreateDocumentRequest
            {
                Title = newTitle,
                CategoryId = 1,
                AccessLevel = AccessLevel.Internal,
                IsDeleted = false,
                TagIds = tagIds,
                CreatorId = 3
            };

            var createRes = await docService.CreateDocumentAsync(createReq);
            if (!createRes.Success)
            {
                Console.WriteLine($"✖ Failed to create document: {createRes.ErrorMessage}");
                return;
            }
            Console.WriteLine($"✔ Created Document ID = {createRes.DocumentId}, {string.Join(",", tagIds)}");



            // —————————————————————————————
            // 5) Create a new Document Version
            Console.WriteLine("► Creating Document Version…");
            var createVerReq = new CreateDocumentVersionRequest
            {
                DocumentId = createRes.DocumentId,
                Content = "Initial draft content",
                Version = "v1.0",
                // CreateDate = DateTime.Now,

            };
            var createVerRes = await verService.CreateDocumentVersionAsync(createVerReq);
            if (!createVerRes.Success)
            {
                Console.WriteLine($"✖ CreateDocumentVersion failed: {createVerRes.ErrorMessage}");
                return;
            }
            Console.WriteLine($"✔ Version created with ID = {createVerRes.DocumentVersionId}");

            // —————————————————————————————
            // 6) Create a new Collection
            Console.WriteLine("► Creating Collection…");
            Console.WriteLine("► Enter Collection name");
            var newCollName = Console.ReadLine();
            var createCollReq = new CreateCollectionRequest
            {
                Name = newCollName,
                CreatorId = userContext.UserId
            };
            var collId = await collService.CreateCollectionAsync(createCollReq);
            Console.WriteLine($"✔ Collection created with ID = {collId.CollectionId}");

            // —————————————————————————————
            // 7) Add Document to the Collection
            Console.WriteLine("► Adding Document to Collection…");
            var addDocsReq = new AddDocumentsToCollectionRequest
            {
                CollectionId = collId.CollectionId,
                DocumentIds = new List<int> { createRes.DocumentId }
            };

            await collService.AddDocumentsToCollectionAsync(addDocsReq);
            Console.WriteLine($"✔ Added document {createRes.DocumentId} to collection {collId.CollectionId}");

        }
    }
}