using OfficeResourcesReservationSystem.Repository.Base;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Employee
{
    public interface IEmployeeRepository : IBaseRepository<Models.Employee, EmployeeFilter, EmployeeUpdate>
    {
    }
}
