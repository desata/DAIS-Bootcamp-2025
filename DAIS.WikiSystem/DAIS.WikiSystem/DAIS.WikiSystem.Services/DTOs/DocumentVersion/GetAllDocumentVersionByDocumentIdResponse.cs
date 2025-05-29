namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class GetAllDocumentVersionByDocumentIdResponse
    {
        public List<DocumentVersionInfo> DocumentVersions { get; set; }

        public int TotalCount { get; set; }
    }
}
