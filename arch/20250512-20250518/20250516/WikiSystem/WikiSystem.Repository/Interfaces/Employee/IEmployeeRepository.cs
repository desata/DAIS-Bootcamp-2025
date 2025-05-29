using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.Employee
{
    public interface IEmployeeRepository : IBaseRepository<Models.Employee, EmployeeFilter, EmployeeUpdate>
    {
    }
}
