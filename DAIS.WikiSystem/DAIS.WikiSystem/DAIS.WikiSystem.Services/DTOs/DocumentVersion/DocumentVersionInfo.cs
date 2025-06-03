namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class DocumentVersionInfo
    {
        public int DocumentVersionId { get; set; }
        public required string FilePath { get; set; }
        public required string Version { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
