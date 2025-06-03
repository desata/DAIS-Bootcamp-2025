namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class GetAllDocumentsResponse
    {
        public List<DocumentInfo> Documents { get; set; }
        public int Count { get; set; }
    }
}
