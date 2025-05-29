namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class DocumentVersionInfo
    {
        public int DocumentVersionId { get; set; }
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreateDate { get; set; }
        public int DocumentId { get; set; }

    }
}
