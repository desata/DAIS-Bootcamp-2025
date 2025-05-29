namespace OfficeResourcesReservationSystem.Services.DTOs.Employee
{
    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public required string FullName { get; set; }
        public required string Username { get; set; }
    }
}
