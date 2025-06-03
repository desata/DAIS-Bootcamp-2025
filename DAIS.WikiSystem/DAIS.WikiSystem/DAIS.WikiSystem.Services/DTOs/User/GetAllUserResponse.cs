namespace DAIS.WikiSystem.Services.DTOs.User
{
    public class GetAllUserResponse
    {
        public List<UserInfo> Users { get; set; }
        public int Count { get; set; }
    }
}
