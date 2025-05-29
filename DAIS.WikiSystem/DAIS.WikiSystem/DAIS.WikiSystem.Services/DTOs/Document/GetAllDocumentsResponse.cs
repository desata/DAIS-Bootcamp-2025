namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class GetAllDocumentsResponse //: DocumentInfo
    {
        public List<DocumentInfo> Documents { get; set; }
        public int TotalCount { get; set; }
    }
}