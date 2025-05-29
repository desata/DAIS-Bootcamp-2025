namespace WikiSystem.Services.DTOs.Document
{
    public class GetAllDocumentsRsponse
    {
        public List<DocumentInfo> Documents { get; set; }

        public int TotalCount { get; set; }
    }
}
