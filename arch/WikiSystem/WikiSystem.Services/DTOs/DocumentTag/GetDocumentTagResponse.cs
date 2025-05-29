namespace WikiSystem.Services.DTOs.DocumentTag
{
    public class GetDocumentTagResponse : DocumentTagInfo
    {
        List<DocumentTagInfo> DocumentTags { get; set; }
        public int TotalCount { get; set; }
    }
}
