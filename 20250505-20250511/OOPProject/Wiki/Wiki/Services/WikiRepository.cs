using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wiki.Models;


namespace Wiki.Services
{
    public class WikiRepository
    {


        private int versionCounter = 0;//TODO should other
        private List<Collection> allCollections = new();

        private List<Document> documents = new();


        public void AddDocument(User user, Document document)  //TODO 
        {
            if (user.Role == Role.Admin || user.Role == Role.Editor)
            {
                document.Versions.Add(new DocumentVersion
                {
                    VersionNumber = versionCounter++,
                    UploadedAt = DateTime.Now
                });
                document.ChangeHistory.Add(new ChangeRegister
                {
                    Timestamp = DateTime.Now,
                    UserId = user.UserId,
                    ChangeDescription = "Initial upload"
                });
                documents.Add(document);
                Console.WriteLine($"Document '{document.Title}' added.");
            }
            else
            {
                Console.WriteLine("Access denied: user cannot add documents.");
            }
        }

        public void AddToCollection(User user, Document document)
        {
            if (!documents.Contains(document))
            {
                documents.Add(document);
                Console.WriteLine($"Document '{document.Title}' added to collection '{user.Name}'.");
            }
            else
            {
                Console.WriteLine($"Document '{document.Title}' is already in the collection.");
            }
        }

        public List<Document> SearchDocumentsbyTag(string tag = null)
        {
            return documents.Where(m =>
    tag == null || m.Tags.Any(t => t.Equals(tag, StringComparison.OrdinalIgnoreCase))
).ToList();
        }

        public List<Document> SearchDocumentsbyTitle(string title = null)
        {
            return documents.Where(m => (title == null || m.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        public List<Document> FindSpecificDocument(string title, string content, int version)
        {
            return documents.FirstOrDefault(d =>
                (d.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
                (d.Content.Contains(content, StringComparison.OrdinalIgnoreCase)) &&
                (d.Versions.Any(x => x.VersionNumber == version))
            
        }

        public Document FindSpecificDocumentByID(string id)
        {
            return documents.FirstOrDefault(d => (d.Id.Any(x => x.Equals(id))));
        }

        public void AddVersion(string documentId)
        {
            var document = documents.FirstOrDefault(m => m.Id == documentId);
            if (document != null)
            {
                var newVersion = new DocumentVersion
                {
                    VersionNumber = document.Versions.Count + 1,
                    UploadedAt = DateTime.Now
                };
                document.Versions.Add(newVersion);
                document.ChangeHistory.Add(new ChangeRegister
                {
                    Timestamp = DateTime.Now,
                    UserId = "system",
                    ChangeDescription = $"Version {newVersion.VersionNumber} added"
                });
                Console.WriteLine($"New version added for {document.Title}");
            }
        }

        public List<ChangeRegister> GetChangeLog(string documentId)
        {
            return documents.FirstOrDefault(m => m.Id == documentId)?.ChangeHistory ?? new();
        }

        public List<Collection> GetUserCollections(User user, List<Collection> allCollections)
        {
            return allCollections.Where(c => c.CreatedBy.Name == user.Name).ToList();
        }

        public void ArchiveOldDocuments(Document document)
        {
            foreach (var doc in documents)
            {
                if (doc.Versions.Count > 2)
                {
                    doc.AccessLevel = AccessLevel.Restricted;
                    doc.ChangeHistory.Add(new ChangeRegister
                    {
                        Timestamp = DateTime.Now,
                        UserId = "system",
                        ChangeDescription = "Archived due to version count"
                    });
                    Console.WriteLine($"Document {doc.Title} archived.");
                }
            }
        }
    }
}