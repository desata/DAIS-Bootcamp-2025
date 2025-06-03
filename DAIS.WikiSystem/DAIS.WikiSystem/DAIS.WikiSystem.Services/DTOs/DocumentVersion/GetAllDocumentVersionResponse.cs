namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class GetAllDocumentVersionResponse
    {
        public List<GetDocumentVersionResponse> DocumentVersions { get; set; }

        public int Count { get; set; }
    }
}
