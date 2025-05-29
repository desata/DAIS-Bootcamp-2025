namespace OfficeResourcesReservationSystem.Services.DTOs.Authorisation
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public int? EmployeeId { get; set; } 
    }
}
